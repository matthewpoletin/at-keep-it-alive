using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class ProgressBarHorizontal : ProgressBarBase
    {
        [SerializeField] private RectTransform _progressImage = null;

        public override void SetProgress(float value)
        {
            _progressImage.anchorMax = new Vector2(value, _progressImage.anchorMax.y);
        }
    }
}