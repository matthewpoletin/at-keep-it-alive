using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class HorizontalProgressBar : MonoBehaviour, IProgressBar
    {
        [SerializeField] private RectTransform _progressImage = null;

        public void SetProgress(float value)
        {
            _progressImage.anchorMax = new Vector2(value, _progressImage.anchorMax.y);
        }
    }
}