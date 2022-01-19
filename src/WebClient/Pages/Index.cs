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
using WebClient.Mappers;
using WebClient.Models;

namespace WebClient.Pages;

// TODO: Refactoring 
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
    private BlazorTimer _payoutTimeTimer;
    private BlazorTimer _autoRefreshTimer;

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
        _payoutTimeTimer.Start();
        await AutoRefresh();
    }
        
    private void SetTemperatureRanges()
    {
        var temperatureLimits = Configuration.GetSection(nameof(TemperatureLimitsOptions))
            .Get<TemperatureLimitsOptions>();

        _temperatureRanges = Mapper.MapTemperatures(temperatureLimits);
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
            _autoRefreshTimer.Start();
            await LocalStorage.SetItemAsStringAsync(LocalStorageKey.AutoRefreshSwitchIsOn.ToString(), "true");
        }
        else 
        { 
            _autoRefreshTimer.Stop();
            await LocalStorage.SetItemAsStringAsync(LocalStorageKey.AutoRefreshSwitchIsOn.ToString(), "false");
        }
    }

    private void SetTimers()
    {
        _autoRefreshTimer = new BlazorTimer(60000);
        _autoRefreshTimer.OnElapsed += UpdateDashboard;

        _payoutTimeTimer = new BlazorTimer(1000);
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
        }
            
        InvokeAsync(StateHasChanged);
    }
}