using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace KnowCrow.AT.KeepItAlive
{
    public static class MusicianAnimationStates
    {
        public static readonly int Idle = Animator.StringToHash("idle");
        public static readonly int Walk = Animator.StringToHash("walk");
        public static readonly int StagePlay = Animator.StringToHash("stage_play");
        public static readonly int StageTired = Animator.StringToHash("stage_tired");
    }

    public class MusicianView : BaseView
    {
        [SerializeField] private MusicianType _musicianType = default;
        [SerializeField] private MusicianData _musicianData = null;
        [SerializeField] private SpriteRenderer _spriteRenderer = null;
        [SerializeField] private Animator _animator = null;

        public MusicianType MusicianType => _musicianType;
        public MusicianData MusicianData => _musicianData;

        private MusicianModel _musicianModel;
        private MusicianPlayingSpot _musicianPlayingSpot;
        private List<Transform> _offStageSpots;

        private Tween _movementTween;

        public void Initialize(MusicianModel musicianModel, MusicianPlayingSpot musicianSpot,
            List<Transform> offStageSpots)
        {
            _musicianModel = musicianModel;
            _musicianPlayingSpot = musicianSpot;
            _offStageSpots = offStageSpots;

            _musicianModel.OnStageStateChanged += OnStageStateChanged;
            _musicianModel.OnManaLevelChanged += OnManaLevelChanged;

            transform.position = GetRandomOffStageSpot().position;
        }

        private void OnStageStateChanged(StageState stageState)
        {
            switch (stageState)
            {
                case StageState.OffStage:
                {
                    OffStage();
                    break;
                }
                case StageState.WalkingToStage:
                {
                    MoveToStage();
                    break;
                }
                case StageState.OnStage:
                {
                    OnStage();
                    break;
                }
                case StageState.WalkingFromStage:
                {
                    MoveFromStage();
                    break;
                }
            }
        }

        private void OnManaLevelChanged(float value)
        {
            if (_musicianModel.StageState == StageState.OnStage && _musicianModel.IsTired)
            {
                _animator.Play(MusicianAnimationStates.StageTired);
            }
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            if (_musicianModel.StageState == StageState.OnStage)
            {
                _musicianModel.ManaLevel -= _musicianModel.Data.ManaLossSpeed * deltaTime;
            }
            else if (_musicianModel.StageState == StageState.OffStage)
            {
                _musicianModel.ManaLevel += _musicianModel.Data.ManaGainSpeed * deltaTime;
            }
        }

        private void OffStage()
        {
            _animator.Play(MusicianAnimationStates.Idle);
        }

        private void OnStage()
        {
            _animator.Play(MusicianAnimationStates.StagePlay);
        }

        private void MoveToStage()
        {
            _movementTween?.Kill();

            _animator.Play(MusicianAnimationStates.Walk);
            Vector3 startPosition = transform.position;
            var endPosition = new Vector3(_musicianPlayingSpot.transform.position.x, startPosition.y, startPosition.z);
            _spriteRenderer.flipX = Vector3.Dot(endPosition - startPosition, Vector3.right) < 0;
            float movementDuration = (endPosition - startPosition).magnitude / _musicianModel.Data.MovementSpeed;
            _movementTween = DOTween.Sequence()
                .Append(transform.DOMove(endPosition, movementDuration))
                .OnComplete(() => _musicianModel.StageState = StageState.OnStage)
                .SetAutoKill(false)
                .Play();
        }

        private void MoveFromStage()
        {
            _movementTween?.Kill();

            _animator.Play(MusicianAnimationStates.Walk);
            Vector3 startPosition = transform.position;
            var offStageSpot = GetRandomOffStageSpot();
            var endPosition = new Vector3(offStageSpot.position.x, startPosition.y, startPosition.z);
            _spriteRenderer.flipX = Vector3.Dot(endPosition - startPosition, Vector3.right) < 0;
            float movementDuration = (endPosition - startPosition).magnitude / _musicianModel.Data.MovementSpeed;
            _movementTween = DOTween.Sequence()
                .Append(transform.DOMove(endPosition, movementDuration))
                .OnComplete(() => _musicianModel.StageState = StageState.OffStage)
                .SetAutoKill(false)
                .Play();
        }

        private Transform GetRandomOffStageSpot()
        {
            return _offStageSpots[Random.Range(0, _offStageSpots.Count)];
        }

        public void CleanUp()
        {
            _musicianModel.StageState = StageState.OffStage;

            _movementTween?.Kill();
            _movementTween = null;

            transform.position = GetRandomOffStageSpot().position;
        }

        public override void Dispose()
        {
            _musicianModel.OnStageStateChanged -= OnStageStateChanged;
            _musicianModel.OnManaLevelChanged -= OnManaLevelChanged;

            _movementTween?.Kill();
            _movementTween = null;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Handles.Label(transform.position, _musicianModel.StageState.ToString());
            }
        }
#endif
    }
}