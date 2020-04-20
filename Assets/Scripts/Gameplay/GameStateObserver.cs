using System;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public enum GameStateChangeReason
    {
        GameLost,
        GameWon
    }

    public class GameStateObserver : IDisposable
    {
        private readonly ImpressionModel _impressionModel;
        private readonly Timer _timer;

        public event Action<GameStateChangeReason> OnGameStateChanged;

        public GameStateObserver(ImpressionModel impressionModel, Timer timer)
        {
            _impressionModel = impressionModel;
            _timer = timer;

            _impressionModel.OnImpressionLevelChanged += OnImpressionLevelChanged;
            _timer.OnTimerElapsed += OnTimerElapsed;
        }

        private void OnImpressionLevelChanged(float impressionLevel)
        {
            if (Mathf.Approximately(impressionLevel, 0f))
            {
                OnGameStateChanged?.Invoke(GameStateChangeReason.GameLost);
            }
        }

        private void OnTimerElapsed()
        {
            OnGameStateChanged?.Invoke(GameStateChangeReason.GameWon);
        }

        public void Dispose()
        {
            _impressionModel.OnImpressionLevelChanged -= OnImpressionLevelChanged;
            _timer.OnTimerElapsed -= OnTimerElapsed;
        }
    }
}