using TMPro;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class BubbleWidget : BaseView
    {
        [SerializeField] private TextMeshProUGUI _text = null;

        private Transform _pivotTransform;
        private Camera _camera;

        public void Initialize(string text, Transform pivotTransform)
        {
            _text.text = text;
            _pivotTransform = pivotTransform;

            // TODO: Fix later
            _camera = GameObject.FindObjectOfType<Camera>();

            UpdatePosition();
        }

        public override void Tick(float deltaTime)
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            Vector3 viewportPosition = _camera.WorldToScreenPoint(_pivotTransform.position);
            transform.position = _camera.ScreenToWorldPoint(viewportPosition);
        }

        public override void Dispose()
        {
            _text.text = string.Empty;
        }
    }
}