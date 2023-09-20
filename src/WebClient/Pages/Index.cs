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

namespace WebClient.Pages;

public partial class Index
{
    [Inject] private IServerData ServerData { get; set; }
    [Inject] private IConfiguration Configuration { get; set; }
    [Inject] private ILocalStorageService LocalStorage { get; set; }

    private CancellationTokenSource _autoRefreshCts = new();
    private RigsActivity _rigsActivity;
    private string _timeLeft = "-";
    private Dictionary<string, Dictionary<string, int>> _temperatureRanges;
    private bool _autoRefreshActive;
    private BlazorTimer _payoutTimeTimer;
    private BlazorTimer _autoRefreshTimer;
    private string _errorMessage = string.Empty;
    private bool _isError;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _isError = false;
            var autoRefreshActive = await LocalStorage.GetItemAsync<string>(LocalStorageKeys.AutoRefreshSwitchIsOn.ToString());
            _autoRefreshActive = autoRefreshActive != null && Convert.ToBoolean(autoRefreshActive);
            
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
        _temperatureRanges = Configuration.GetSection("TemperatureLimitsOptions")
            .Get<Dictionary<string, Dictionary<string,int>>>();
    }

    private async Task GetNiceHashData()
    {
        var result = await ServerData.GetNiceHashAsync(_autoRefreshCts.Token);

        if (result.IsSuccess) _rigsActivity = RigsActivity.Clone(result.Value);
        else
        {
            _autoRefreshTimer.Stop();
            _payoutTimeTimer.Stop();
            _autoRefreshActive = false;
            _errorMessage = result.Errors[0].Message;
            _isError = true;
        }
        
        StateHasChanged();
    }
    private async Task AutoRefresh()
    {
        _autoRefreshCts.Cancel();
        _autoRefreshCts = new CancellationTokenSource();

        if (_autoRefreshActive)
        {
            _autoRefreshTimer.Reset();
            _autoRefreshTimer.Start();
            await LocalStorage.SetItemAsStringAsync(LocalStorageKeys.AutoRefreshSwitchIsOn.ToString(), "true");
        }
        else 
        { 
            _autoRefreshTimer.Stop();
            await LocalStorage.SetItemAsStringAsync(LocalStorageKeys.AutoRefreshSwitchIsOn.ToString(), "false");
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
        if (_rigsActivity is null) return;
            
        var timeLeft = _rigsActivity.NextPayoutTimestamp.Subtract(DateTime.Now);
            
        if (timeLeft.TotalSeconds > 0) _timeLeft = timeLeft.ToString(@"hh\:mm\:ss");
        else 
        { 
            _rigsActivity = null;
            InvokeAsync(async () => { await Start(); });
        }
            
        InvokeAsync(StateHasChanged);
    }
}