using System;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace WebClient.Domain
{
    public class BlazorTimer
    {
        private Timer _timer;
        private CancellationToken _cancellationToken;

        public void SetTimer(double interval, CancellationToken cancellationToken = default)
        {
            _timer = new Timer(interval);
            _cancellationToken = cancellationToken;
            _timer.Elapsed += NotifyTimerElapsed;
            _timer.Enabled = true;
        }

        public event Action OnElapsed;

        private void NotifyTimerElapsed(object source, ElapsedEventArgs e)
        {
            if(_cancellationToken.IsCancellationRequested) Dispose();
            OnElapsed?.Invoke();
        }
        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}