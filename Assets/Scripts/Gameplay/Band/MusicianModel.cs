using System;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public enum MusicianType
    {
        Bass = 0,
        Drums = 1,
        Piano = 2,
        Trumpet = 3,
    }

    public class MusicianModel
    {
        public MusicianType MusicianType { get; private set; }

        private float _manaLevel;

        public event Action<float> OnManaLevelChanged;

        public float ManaLevel
        {
            get => _manaLevel;
            set
            {
                _manaLevel = Mathf.Clamp(value, 0f, 1f);
                OnManaLevelChanged?.Invoke(_manaLevel);
            }
        }

        public MusicianModel(MusicianType musicianType)
        {
            MusicianType = musicianType;
        }
    }
}