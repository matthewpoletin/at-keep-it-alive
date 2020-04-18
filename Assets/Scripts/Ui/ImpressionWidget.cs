using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class ImpressionWidget : BaseView
    {
        [SerializeField] private VerticalProgressBar _verticalProgressBar = null;

        private ImpressionModel _impressionModel;

        public void Initialize(ImpressionModel impressionModel)
        {
            _impressionModel = impressionModel;

            _impressionModel.OnImpressionLevelChanged += OnImpressionLevelChanged;
        }

        private void OnImpressionLevelChanged(float impressionLevel)
        {
            _verticalProgressBar.SetProgress(impressionLevel);
        }

        public override void Dispose()
        {
            _impressionModel.OnImpressionLevelChanged -= OnImpressionLevelChanged;
        }
    }
}