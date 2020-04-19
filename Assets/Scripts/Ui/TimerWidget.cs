using System;
using TMPro;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class TimerWidget : BaseView
    {
        [SerializeField] private VerticalProgressBar _verticalProgressBar = null;
        [SerializeField] private TextMeshProUGUI _text = null;

        private const float TIME_START = 18;
        private const float TIME_END = 25;

        private Timer _timer;

        public void Initialize(Timer timer)
        {
            _timer = timer;

            _timer.OnTimerTick += OnTimerTick;
            _timer.OnTimerElapsed += OnTimerElapsed;
        }

        private void OnTimerTick(float value)
        {
            float progress = Mathf.Clamp(1 - value / _timer.Duration, 0f, 1f);
            _verticalProgressBar.SetProgress(progress);
            _text.text = new DateTime().AddHours(TIME_START)
                .AddHours((TIME_END - TIME_START) * progress)
                .ToShortTimeString();
        }

        private void OnTimerElapsed()
        {
            _timer.OnTimerTick -= OnTimerTick;
        }

        public override void Dispose()
        {
            _timer.OnTimerTick -= OnTimerTick;
        }
    }
}