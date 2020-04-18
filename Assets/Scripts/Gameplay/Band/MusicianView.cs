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

        private Tween _movementTween;

        public void Initialize(MusicianModel musicianModel)
        {
            _musicianModel = musicianModel;

            _musicianModel.OnStageStateChanged += OnStageStateChanged;
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

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            if (_musicianModel.StageState == StageState.OnStage)
            {
                _musicianModel.ManaLevel -= _musicianModel.Data.ManaLossSpeed * deltaTime;
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
            var endPosition = new Vector3(startPosition.x + 10, startPosition.y, startPosition.z);
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
            var endPosition = new Vector3(startPosition.x - 10, startPosition.y, startPosition.z);
            _spriteRenderer.flipX = Vector3.Dot(endPosition - startPosition, Vector3.right) < 0;
            float movementDuration = (endPosition - startPosition).magnitude / _musicianModel.Data.MovementSpeed;
            _movementTween = DOTween.Sequence()
                .Append(transform.DOMove(endPosition, movementDuration))
                .OnComplete(() => _musicianModel.StageState = StageState.OffStage)
                .SetAutoKill(false)
                .Play();
        }

        public override void Dispose()
        {
            _musicianModel.OnStageStateChanged -= OnStageStateChanged;

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