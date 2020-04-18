using UnityEngine;
using UnityEngine.UI;

namespace KnowCrow.AT.KeepItAlive
{
    public class MusicianWidgetItemView : BaseView
    {
        [SerializeField] private MusicianType _musicianType = default;
        [SerializeField] private Image _stageIcon = null;
        [SerializeField] private HorizontalProgressBar _progressBar = null;
        [SerializeField] private Button _button = null;

        public MusicianType MusicianType => _musicianType;

        private MusicianModel _musicianModel;

        public void Initialize(MusicianModel musicianModel)
        {
            _musicianModel = musicianModel;

            _button.onClick.AddListener(OnMusicianButtonClick);
            _musicianModel.OnManaLevelChanged += OnManaLevelChanged;
            OnManaLevelChanged(_musicianModel.ManaLevel);
            _musicianModel.OnStageStateChanged += OnStageStateChanged;
            OnStageStateChanged(_musicianModel.StageState);
        }

        private void OnMusicianButtonClick()
        {
            _musicianModel.ToggleStageState();
        }

        private void OnManaLevelChanged(float manaLevel)
        {
            _progressBar.SetProgress(manaLevel);
        }

        private void OnStageStateChanged(StageState stageState)
        {
            _stageIcon.gameObject.SetActive(stageState == StageState.OnStage);
        }

        public override void Dispose()
        {
            _button.onClick.RemoveListener(OnMusicianButtonClick);
            _musicianModel.OnManaLevelChanged -= OnManaLevelChanged;
            _musicianModel.OnStageStateChanged -= OnStageStateChanged;
        }
    }
}