using Microsoft.AspNetCore.Components;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebClient.Services;
using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using WebClient.Domain;
using WebClient.Models;

namespace WebClient.Pages
{
    // TODO: Refactoring - introduce mediator pattern, decouple responsibilities
    public partial class Index
    {
        [Inject] private IDataService DataService { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private ILocalStorageService LocalStorage { get; set; }

        private CancellationTokenSource _autoRefreshCts = new();
        private NiceHashData _niceHashData;
        private string _timeLeft = "-";
        private Dictionary<string, Dictionary<string, int>> _temperatureRanges;
        private bool _autoRefreshActive;

        
        private readonly BlazorTimer _payoutTimeTimer = new();
        private readonly BlazorTimer _autoRefreshTimer = new();

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
             var temperatureLimits = Configuration.GetSection(nameof(TemperatureLimitsOptions))
                .Get<TemperatureLimitsOptions>();

            _temperatureRanges = Mappers.MapTemperatures(temperatureLimits);
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
                SetTimers();
                await LocalStorage.SetItemAsStringAsync(LocalStorageKey.AutoRefreshSwitchIsOn.ToString(), "true");
            }
            else 
            { 
                //_autoRefreshTimer.Change(Timeout.Infinite, Timeout.Infinite);
                await LocalStorage.SetItemAsStringAsync(LocalStorageKey.AutoRefreshSwitchIsOn.ToString(), "false");
            }
        }

        private void SetTimers()
        {
            _autoRefreshTimer.SetTimer(10000, _autoRefreshCts.Token);
            _autoRefreshTimer.OnElapsed += UpdateDashboard;
            
            _payoutTimeTimer.SetTimer(1000, _autoRefreshCts.Token);
            _payoutTimeTimer.OnElapsed += UpdateCountdown;
        }

        private void UpdateDashboard()
        {
            InvokeAsync(async () =>
            {
                await GetNiceHashData();
                StateHasChanged();
            });
        }

        private void UpdateCountdown()
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
        }
    }
}