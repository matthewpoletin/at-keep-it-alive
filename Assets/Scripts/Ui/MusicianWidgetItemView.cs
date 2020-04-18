using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class MusicianWidgetItemView : BaseView
    {
        [SerializeField] private MusicianType _musicianType = default;
        [SerializeField] private HorizontalProgressBar _progressBar = null;

        public MusicianType MusicianType => _musicianType;

        private MusicianModel _musicianModel;

        public void Initialize(MusicianModel musicianModel)
        {
            _musicianModel = musicianModel;

            _musicianModel.OnManaLevelChanged += OnManaLevelChanged;
        }

        private void OnManaLevelChanged(float manaLevel)
        {
            _progressBar.SetProgress(manaLevel);
        }

        public override void Dispose()
        {
            _musicianModel.OnManaLevelChanged -= OnManaLevelChanged;
        }
    }
}