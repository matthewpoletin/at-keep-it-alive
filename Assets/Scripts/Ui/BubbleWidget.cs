using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KnowCrow.AT.KeepItAlive
{
    public class BubbleWidget : BaseView
    {
        [SerializeField] private TextMeshProUGUI _text = null;
        [SerializeField] private Button _button = null;

        private Transform _pivotTransform;
        private Camera _mainCamera;
        private float _fadeTimeout;
        public bool IsPositive { get; private set; }
        private Action<BubbleWidget> _onBubbleClick;
        private Action<BubbleWidget> _onBubbleFaded;

        public void Initialize(Transform pivotTransform, Camera mainCamera, string text,
            bool isPositive, float fadeDuration, Action<BubbleWidget> onBubbleClick, Action<BubbleWidget> onBubbleFaded)
        {
            _pivotTransform = pivotTransform;
            _mainCamera = mainCamera;
            IsPositive = isPositive;
            _fadeTimeout = fadeDuration;
            _onBubbleClick = onBubbleClick;
            _onBubbleFaded = onBubbleFaded;

            _text.text = text;

#if UNITY_EDITOR
            foreach (Image image in gameObject.GetComponentsInChildren<Image>())
            {
                image.color = IsPositive ? Color.green : Color.red;
            }
#endif

            UpdatePosition();

            _button.onClick.AddListener(OnBubbleClick);
        }

        private void OnBubbleClick()
        {
            _button.onClick.RemoveListener(OnBubbleClick);
            _onBubbleClick.Invoke(this);
        }

        public override void Tick(float deltaTime)
        {
            if (_fadeTimeout >= 0)
            {
                _fadeTimeout -= deltaTime;
                if (_fadeTimeout < 0)
                {
                    _onBubbleFaded.Invoke(this);
                }
            }

            UpdatePosition();
        }

        private void UpdatePosition()
        {
            Vector3 viewportPosition = _mainCamera.WorldToScreenPoint(_pivotTransform.position);
            transform.position = _mainCamera.ScreenToWorldPoint(viewportPosition);
        }

        public override void Dispose()
        {
            _button.onClick.RemoveAllListeners();
            _text.text = string.Empty;
        }
    }
}