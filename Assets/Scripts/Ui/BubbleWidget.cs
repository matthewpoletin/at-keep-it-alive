using TMPro;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class BubbleWidget : BaseView
    {
        [SerializeField] private TextMeshProUGUI _text = null;

        private Transform _pivotTransform;

        public void Initialize(string text, Transform pivotTransform)
        {
            _text.text = text;
            _pivotTransform = pivotTransform;
        }

        public override void Tick(float deltaTime)
        {
            // TODO: Fix later
            transform.position = _pivotTransform.transform.position;
        }
    }
}