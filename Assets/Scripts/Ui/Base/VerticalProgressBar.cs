using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class VerticalProgressBar : MonoBehaviour, IProgressBar
    {
        [SerializeField] private RectTransform _progressImage = null;

        public void SetProgress(float value)
        {
            _progressImage.anchorMax = new Vector2(_progressImage.anchorMax.x, Mathf.Clamp(value, 0f, 1f));
        }
    }
}