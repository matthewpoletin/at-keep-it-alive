using TMPro;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class ImpressionWidget : BaseView
    {
        private const string PROGRESS_TEXT_FORMAT = "{0}/{1}";

        [SerializeField] private ProgressBarBase _verticalProgressBar = null;
        [SerializeField] private TextMeshProUGUI _text = null;

        private ImpressionModel _impressionModel;

        public void Initialize(ImpressionModel impressionModel)
        {
            _impressionModel = impressionModel;

            _impressionModel.OnImpressionLevelChanged += OnImpressionLevelChanged;
        }

        private void OnImpressionLevelChanged(float impressionLevel)
        {
            _verticalProgressBar.SetProgress(impressionLevel);
            int maxProgress = 100;
            string textText = string.Format(PROGRESS_TEXT_FORMAT, (int) (impressionLevel * maxProgress), maxProgress);
            _text.text = textText;
        }

        public override void Dispose()
        {
            _impressionModel.OnImpressionLevelChanged -= OnImpressionLevelChanged;
        }
    }
}