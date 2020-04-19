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
        private Action<BubbleWidget> _onBubbleClick;

        public void Initialize(Transform pivotTransform, Camera mainCamera, string text,
            Action<BubbleWidget> onBubbleClick)
        {
            _pivotTransform = pivotTransform;
            _mainCamera = mainCamera;
            _onBubbleClick = onBubbleClick;

            _text.text = text;

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