using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class GameplayUiView : BaseView
    {
        [SerializeField] private ImpressionWidget _impressionWidget = null;
        [SerializeField] private List<MusicianWidgetItemView> _musicianWidgets = null;
        [SerializeField] private RectTransform _bubbleContainer = null;
        [SerializeField] private GameObject _bubblePrefab = null;
        [SerializeField] private GameObject _gameInfo = null;

        private readonly List<BubbleWidget> _bubbles = new List<BubbleWidget>();

        public void Initialize(ImpressionModel impressionModel, List<MusicianModel> musicians)
        {
            _gameInfo.SetActive(false);

            _impressionWidget.Initialize(impressionModel);
            foreach (MusicianWidgetItemView musicianWidget in _musicianWidgets)
            {
                MusicianModel musicianModel = musicians.FirstOrDefault(model => model.MusicianType == musicianWidget.MusicianType);
                if (musicianModel == null)
                {
                    Debug.LogError("MusicianModel not found");
                }
                musicianWidget.Initialize(musicianModel);
            }
        }

        public void CreateBubble(string text, Transform pivotTransform)
        {
            GameObject bubble = Instantiate(_bubblePrefab, _bubbleContainer);
            var bubbleWidget = bubble.GetComponent<BubbleWidget>();
            bubbleWidget.Initialize(text, pivotTransform);
            _bubbles.Add(bubbleWidget);
        }

        public override void Dispose()
        {
            foreach (BubbleWidget bubbleWidget in _bubbles)
            {
                bubbleWidget.Dispose();
            }

            _bubbles.Clear();
        }

        public void ShowGameInfo()
        {
            _gameInfo.SetActive(true);
        }

        public void HideGameInfo()
        {
            _gameInfo.SetActive(false);
        }
    }
}