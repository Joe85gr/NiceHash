using Microsoft.AspNetCore.Components;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebClient.Services;
using Blazored.LocalStorage;
using WebClient.Models;

namespace WebClient.Pages
{
    // TODO: Refactoring - introduce mediator pattern, decouple responsibilities
    public partial class Index
    {
        [Inject] private IDataService DataService { get; set; }

        private CancellationTokenSource _autoRefreshCts = new();
        private NiceHashData _niceHashData;
        private string _timeLeft = "-";
        private Dictionary<string, Dictionary<string, int>> _temperatureRanges;
        private bool _autoRefreshActive;
        private Timer _autoRefreshTimer;
        private Timer _payoutTimeTimer;

        [Inject] private ILocalStorageService LocalStorage { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var autoRefreshActive = await LocalStorage.GetItemAsync<string>(LocalStorageKey.AutoRefreshSwitchIsOn.ToString());
                if (autoRefreshActive != null) _autoRefreshActive = Convert.ToBoolean(autoRefreshActive);

                SetTemperatureRanges();
                SetTimers();
                await Start();
            }
        }

        private async Task Start()
        {
            await GetNiceHashData();
            await AutoRefresh();
        }
        
        private void SetTemperatureRanges()
        {
            _temperatureRanges = new Dictionary<string, Dictionary<string, int>>()
            {
                { "GPU Temperture", new Dictionary<string, int>{{ "danger", 82 }, { "warning", 72 }} },
                { "VRAM Temperture", new Dictionary<string, int>{{ "danger", 102 }, { "warning", 96 }} },
            };
        }

        private async Task GetNiceHashData()
        {
            _niceHashData = await DataService.GetNiceHashAsync(_autoRefreshCts.Token);
            StateHasChanged();
        }
        private async Task AutoRefresh()
        {
            _autoRefreshCts.Cancel();
            _autoRefreshCts = new CancellationTokenSource();

            if (_autoRefreshActive)
            {
                _autoRefreshTimer.Change(10000, 10000);
                await LocalStorage.SetItemAsStringAsync(LocalStorageKey.AutoRefreshSwitchIsOn.ToString(), "true");
            }
            else 
            { 
                _autoRefreshTimer.Change(Timeout.Infinite, Timeout.Infinite);
                await LocalStorage.SetItemAsStringAsync(LocalStorageKey.AutoRefreshSwitchIsOn.ToString(), "false");
            }
        }

        private void SetTimers()
        {
            _autoRefreshTimer = new Timer(_ =>
            {
                InvokeAsync(async () =>
                {
                    await GetNiceHashData();
                    StateHasChanged();
                });
            }, _autoRefreshCts.Token, Timeout.Infinite, Timeout.Infinite);

            _payoutTimeTimer = new Timer(_ =>
            {
                if (_niceHashData is null) return;
                
                var timeLeft = _niceHashData.NextPayoutTimestamp.Subtract(DateTime.Now);

                if (timeLeft.TotalSeconds > 0) _timeLeft = timeLeft.ToString(@"hh\:mm\:ss");
                else 
                { 
                    _niceHashData = null;
                    InvokeAsync(async () => { await Start(); });
                    Task.Delay(10000).Wait();
                }

                InvokeAsync(StateHasChanged);

            }, null, 0, 1000);
        }
    }
}