using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    [CreateAssetMenu(fileName = "GameParams", menuName = "Params/GameParams", order = 10)]
    public class GameParams : ScriptableObject
    {
        [SerializeField] private float _sessionDurationSec = 60f;
        [SerializeField] private float _impressionLossSpeed = 60f;
        [SerializeField] private float _positiveClickPointsLoss = 1f;
        [SerializeField] private float _negativeClickPointsGain = 1f;

        public float SessionDurationSec => _sessionDurationSec;
        public float ImpressionLossSpeed => _impressionLossSpeed;
        public float PositiveClickPointsLoss => _positiveClickPointsLoss;
        public float NegativeClickPointsGain => _negativeClickPointsGain;
    }
}