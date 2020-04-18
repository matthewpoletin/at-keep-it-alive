using System;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class ImpressionModel
    {
        public event Action<float> OnImpressionLevelChanged;

        private float _impressionLevel;

        public float ImpressionLevel
        {
            get => _impressionLevel;
            set
            {
                _impressionLevel = Mathf.Clamp(value, 0, 1);
                OnImpressionLevelChanged?.Invoke(_impressionLevel);
            }
        }

        public ImpressionModel()
        {
            _impressionLevel = 1f;
        }
    }
}