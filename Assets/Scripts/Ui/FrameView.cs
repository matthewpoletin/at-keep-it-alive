using TMPro;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class FrameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text = null;

        public void SetText(string value)
        {
            _text.text = value;
        }
    }
}