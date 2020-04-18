using System;

namespace KnowCrow.AT.KeepItAlive
{
    public class Timer : ITick
    {
        private float _timeLeft;

        public event Action<float> OnTimerTick;
        public event Action OnTimerElapsed;

        private bool _elapsed;

        private bool _paused;

        public Timer(float duration)
        {
            Reset(duration);
        }

        public void Reset(float duration)
        {
            _timeLeft = duration;
        }

        public void Tick(float deltaTime)
        {
            if (_paused || _elapsed)
            {
                return;
            }

            _timeLeft -= deltaTime;
            OnTimerTick?.Invoke(_timeLeft);

            if (!_elapsed && _timeLeft < 0)
            {
                _elapsed = true;
                OnTimerElapsed?.Invoke();
            }
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }
    }
}