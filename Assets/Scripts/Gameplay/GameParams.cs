using UnityEngine;
using UnityEngine.Serialization;

namespace KnowCrow.AT.KeepItAlive
{
    [CreateAssetMenu(fileName = "GameParams", menuName = "Params/GameParams", order = 10)]
    public class GameParams : ScriptableObject
    {
        [SerializeField] private float _sessionDurationSec = 60f;

        [FormerlySerializedAs("_impressionLossSpeed")]
        [SerializeField]
        private float _passiveImpressionLossSpeed = 0.01f;

        [SerializeField] private float _positiveClickPointsLoss = 1f;
        [SerializeField] private float _negativeClickPointsGain = 1f;
        [SerializeField] private float _positiveFadePointsGain = 1f;
        [SerializeField] private float _negativeFadePointsLoss = 1f;
        [SerializeField] private float _positiveBubbleFadeTimeSec = 10f;
        [SerializeField] private float _negativeBubbleFadeTimeSec = 10f;

        [Header("Musicians out of stage")]
        [SerializeField] private float _musiciansOutOfStage0 = 0.0f;
        [SerializeField] private float _musiciansOutOfStage1 = 0.0f;
        [SerializeField] private float _musiciansOutOfStage2 = 0.0f;
        [SerializeField] private float _musiciansOutOfStage3 = 0.0f;
        [SerializeField] private float _musiciansOutOfStage4 = 0.0f;

        public float SessionDurationSec => _sessionDurationSec;
        public float PassiveImpressionLossSpeed => _passiveImpressionLossSpeed;
        public float PositiveClickPointsLoss => _positiveClickPointsLoss;
        public float NegativeClickPointsGain => _negativeClickPointsGain;
        public float PositiveFadePointsGain => _positiveFadePointsGain;
        public float NegativeFadePointsLoss => _negativeFadePointsLoss;
        public float PositiveBubbleFadeTimeSec => _positiveBubbleFadeTimeSec;
        public float NegativeBubbleFadeTimeSec => _negativeBubbleFadeTimeSec;

        public float MusiciansOutOfStage0 => _musiciansOutOfStage0;
        public float MusiciansOutOfStage1 => _musiciansOutOfStage1;
        public float MusiciansOutOfStage2 => _musiciansOutOfStage2;
        public float MusiciansOutOfStage3 => _musiciansOutOfStage3;
        public float MusiciansOutOfStage4 => _musiciansOutOfStage4;
    }
}