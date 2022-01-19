using System;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace WebClient.Domain;

public class BlazorTimer
{
    private Timer _timer;
    private CancellationToken _cancellationToken;

    public BlazorTimer(double interval, CancellationToken cancellationToken = default)
    {
        _timer = new Timer(interval);
        _cancellationToken = cancellationToken;
        _timer.Elapsed += NotifyTimerElapsed;
    }

    public event Action OnElapsed;

    public void Start()
    {
        _timer.Enabled = true;
    }
    
    public void Stop()
    {
        _timer.Enabled = false;
    }

    private void NotifyTimerElapsed(object source, ElapsedEventArgs e)
    {
        if (_cancellationToken.IsCancellationRequested) _timer.Dispose();
        OnElapsed?.Invoke();
    }
}
