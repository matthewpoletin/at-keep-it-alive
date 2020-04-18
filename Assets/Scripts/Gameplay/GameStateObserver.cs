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

        public event Action<GameStateChangeReason> OnGameStateChanged;

        public GameStateObserver(ImpressionModel impressionModel)
        {
            _impressionModel = impressionModel;

            _impressionModel.OnImpressionLevelChanged += OnImpressionLevelChanged;
        }

        private void OnImpressionLevelChanged(float impressionLevel)
        {
            if (Mathf.Approximately(impressionLevel, 0f))
            {
                OnGameStateChanged?.Invoke(GameStateChangeReason.GameLost);
            }
        }

        private void OnTimerOver()
        {
            OnGameStateChanged?.Invoke(GameStateChangeReason.GameWon);
        }

        public void Dispose()
        {
            _impressionModel.OnImpressionLevelChanged -= OnImpressionLevelChanged;
        }
    }
}