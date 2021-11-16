using Microsoft.AspNetCore.Components;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebClient.Services;

namespace WebClient.Pages
{
    public partial class Index
    {
        [Inject] private IDataService DataService { get; set; }

        private CancellationTokenSource _autoRefreshCts = new();
        private NiceHashData _niceHashData;
        private string TimeLeft = "-";
        private Dictionary<string, Dictionary<string, int>> TempratureRanges;
        private bool AutoRefreshActive = true;
        private Timer AutoRefreshTimer;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                SetTemperatureRanges();
                SetTimers();
                await Start();
            }
        }

        private async Task Start()
        {
            await GetNiceHashData();
            AutoRefresh();
        }
        
        private void SetTemperatureRanges()
        {
            TempratureRanges = new Dictionary<string, Dictionary<string, int>>()
            {
                { "GPU Temperture", new Dictionary<string, int>{{ "danger", 90 }, { "warning", 85 }} },
                { "VRAM Temperture", new Dictionary<string, int>{{ "danger", 100 }, { "warning", 96 }} },
            };
        }

        private async Task GetNiceHashData()
        {
            _niceHashData = await DataService.GetNiceHashAsync(_autoRefreshCts.Token);
            StateHasChanged();
        }
        private void AutoRefresh()
        {
            _autoRefreshCts.Cancel();
            _autoRefreshCts = new();

            if (AutoRefreshActive)
            {
                AutoRefreshTimer.Change(10000, 10000);
            }

            else AutoRefreshTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void SetTimers()
        {
            AutoRefreshTimer = new Timer(new TimerCallback(_ =>
            {
                InvokeAsync(async () =>
                {
                    await GetNiceHashData();
                    StateHasChanged();
                });
            }), _autoRefreshCts.Token, Timeout.Infinite, Timeout.Infinite);

            var payoutTimeTimer = new Timer(new TimerCallback(_ =>
            {
                if (_niceHashData is null) return;
                
                var timeLeft = _niceHashData.NextPayoutTimestamp.Subtract(DateTime.Now);

                if (timeLeft.TotalSeconds > 0) TimeLeft = timeLeft.ToString(@"hh\:mm\:ss");
                else { 
                    _niceHashData = null;
                    InvokeAsync(async () => { await Start(); });
                    Task.Delay(10000).Wait();
                }

                InvokeAsync(() =>
                {
                    StateHasChanged();
                });

            }), null, 0, 1000);
        }
    }
}