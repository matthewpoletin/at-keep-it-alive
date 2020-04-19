using DG.Tweening;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class EnvironmentView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _shadeImage = null;

        private Sequence _shadeTween = null;

        public void ShowShade(float duration = 1f)
        {
            _shadeTween?.Kill();

            _shadeTween = DOTween.Sequence()
                .OnStart(() =>
                {
                    Color color = _shadeImage.color;
                    color = new Color(color.r, color.g, color.b, 0f);
                    _shadeImage.color = color;
                })
                .Append(_shadeImage.DOFade(1f, duration))
                .Play();
        }

        public void HideFade(float duration = 1f)
        {
            _shadeTween?.Kill();

            _shadeTween = DOTween.Sequence()
                .OnStart(() =>
                {
                    Color color = _shadeImage.color;
                    color = new Color(color.r, color.g, color.b, 1f);
                    _shadeImage.color = color;
                })
                .Append(_shadeImage.DOFade(0f, duration))
                .Play();
        }
    }
}