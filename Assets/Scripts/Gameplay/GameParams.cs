using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    [CreateAssetMenu(fileName = "GameParams", menuName = "Params/GameParams", order = 10)]
    public class GameParams : ScriptableObject
    {
        [SerializeField] private float _sessionDurationSec = 60f;
        [SerializeField] private float _impressionGainPerMusicianSpeed = 0.01f;
        [SerializeField] private float _impressionLossPerMusicianTired = 0.01f;
        [SerializeField] private float _impressionLossSpeed = 0.01f;
        [SerializeField] private float _positiveClickPointsLoss = 1f;
        [SerializeField] private float _negativeClickPointsGain = 1f;
        [SerializeField] private float _positiveFadePointsGain = 1f;
        [SerializeField] private float _negativeFadePointsLoss = 1f;
        [SerializeField] private float _positiveBubbleFadeTimeSec = 10f;
        [SerializeField] private float _negativeBubbleFadeTimeSec = 10f;

        public float SessionDurationSec => _sessionDurationSec;
        public float ImpressionGainPerMusicianSpeed => _impressionGainPerMusicianSpeed;
        public float ImpressionLossPerMusicianTired => _impressionLossPerMusicianTired;
        public float ImpressionLossSpeed => _impressionLossSpeed;
        public float PositiveClickPointsLoss => _positiveClickPointsLoss;
        public float NegativeClickPointsGain => _negativeClickPointsGain;
        public float PositiveFadePointsGain => _positiveFadePointsGain;
        public float NegativeFadePointsLoss => _negativeFadePointsLoss;
        public float PositiveBubbleFadeTimeSec => _positiveBubbleFadeTimeSec;
        public float NegativeBubbleFadeTimeSec => _negativeBubbleFadeTimeSec;
    }
}