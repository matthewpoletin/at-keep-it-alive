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

    public enum StageState
    {
        OffStage = 0,
        WalkingToStage = 1,
        OnStage = 2,
        WalkingFromStage = 3,
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

        public event Action<StageState> OnStageStateChanged;


        private StageState _stageState = StageState.OffStage;

        public StageState StageState
        {
            get => _stageState;
            set
            {
                _stageState = value;
                OnStageStateChanged?.Invoke(_stageState);
            }
        }

        public void ToggleStageState()
        {
            switch (_stageState)
            {
                case StageState.OffStage:
                {
                    StageState = StageState.WalkingToStage;
                    break;
                }
                case StageState.WalkingToStage:
                {
                    StageState = StageState.WalkingFromStage;
                    break;
                }
                case StageState.OnStage:
                {
                    StageState = StageState.WalkingFromStage;
                    break;
                }
                case StageState.WalkingFromStage:
                {
                    StageState = StageState.WalkingToStage;
                    break;
                }
            }
        }

        
        public MusicianModel(MusicianType musicianType)
        {
            MusicianType = musicianType;
            ManaLevel = 1f;
        }
    }
}