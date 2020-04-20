using System;
using System.Collections.Generic;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class GameplayUiView : BaseView
    {
        [SerializeField] private ImpressionWidget _impressionWidget = null;
        [SerializeField] private TimerWidget _timerWidget = null;
        [SerializeField] private MusicianWidget _musicianWidget = null;
        [SerializeField] private RectTransform _bubbleContainer = null;
        [SerializeField] private GameObject _bubblePrefab = null;
        [SerializeField] private GameObject _activeStateContainer = null;
        [SerializeField] private GameObject _gameInfo = null;
        [SerializeField] private GameObject _victoryScreen = null;
        [SerializeField] private GameObject _defeatScreen = null;

        private readonly List<BubbleWidget> _bubbles = new List<BubbleWidget>();
        private Camera _mainCamera;

        public void Initialize(Camera uiCamera, ImpressionModel impressionModel, Timer timer,
            List<MusicianModel> musicians)
        {
            _mainCamera = uiCamera;

            _impressionWidget.Initialize(impressionModel);
            _timerWidget.Initialize(timer);
            _musicianWidget.Initialize(musicians);
        }

        public override void Tick(float deltaTime)
        {
            // TODO: Temporary hack because bubble is removed in tick, but im too tired
            for (int index = _bubbles.Count - 1; index >= 0; index--)
            {
                BubbleWidget bubble = _bubbles[index];
                bubble.Tick(deltaTime);
            }
        }

        public BubbleWidget CreateBubble(Transform pivotTransform, string text, bool isPositive, float fadeDuration,
            Action<BubbleWidget> onBubbleClick, Action<BubbleWidget> onBubbleFaded)
        {
            GameObject bubble = Instantiate(_bubblePrefab, _bubbleContainer);
            var bubbleWidget = bubble.GetComponent<BubbleWidget>();
            bubbleWidget.Initialize(pivotTransform, _mainCamera, text, isPositive, fadeDuration, onBubbleClick,
                onBubbleFaded);
            _bubbles.Add(bubbleWidget);
            return bubbleWidget;
        }

        public void HideBubble(BubbleWidget bubbleWidget)
        {
            bubbleWidget.gameObject.SetActive(false);
            _bubbles.Remove(bubbleWidget);
        }

        public void ShowVictoryScreen()
        {
            _victoryScreen.SetActive(true);
        }

        public void ShowDefeatScreen()
        {
            _defeatScreen.SetActive(true);
        }

        public void HideAllScreens()
        {
            _victoryScreen.SetActive(false);
            _defeatScreen.SetActive(false);
        }

        public void ShowActiveState()
        {
            _activeStateContainer.SetActive(true);
        }

        public void HideActiveState()
        {
            _activeStateContainer.SetActive(false);
        }

        public void ShowGameInfo()
        {
            _gameInfo.SetActive(true);
        }

        public void HideGameInfo()
        {
            _gameInfo.SetActive(false);
        }

        public override void Dispose()
        {
            foreach (BubbleWidget bubbleWidget in _bubbles)
            {
                bubbleWidget.Dispose();
            }

            _bubbles.Clear();
        }
    }
}