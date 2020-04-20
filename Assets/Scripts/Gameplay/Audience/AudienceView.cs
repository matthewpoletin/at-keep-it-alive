using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class AudienceView : BaseView
    {
        private class BubbleData
        {
            public BubbleWidget Widget;
            public float TimeLeft;
            public bool IsPositive { get; }

            public BubbleData(BubbleWidget bubbleWidget, float timeLeft, bool isPositive)
            {
                Widget = bubbleWidget;
                TimeLeft = timeLeft;
                IsPositive = isPositive;
            }
        }

        [SerializeField] private List<TableView> _tablesContainer = null;
        [SerializeField] private AudienceReviews _audienceReviews = null;

        private GameplayUiView _uiView;
        private ImpressionModel _impressionModel;
        private GameParams _gameParams;

        private const float BubbleAppearTimeout = 2.5f;


        private readonly Dictionary<Transform, BubbleData>
            _listererToBubbleDict = new Dictionary<Transform, BubbleData>();

        private float _reviewCountdown = 0f;

        private bool _isActive;

        public void Initialize(GameplayUiView uiView, ImpressionModel impressionModel, GameParams gameParams)
        {
            _uiView = uiView;
            _impressionModel = impressionModel;
            _gameParams = gameParams;

            foreach (TableView tableView in _tablesContainer)
            {
                foreach (Transform pivotPoint in tableView.MessagePivotPoint)
                {
                    if (tableView.MessagePivotPoint == null)
                    {
                        Debug.Log($"Message pivot point value is empty for '{tableView}'");
                    }

                    _listererToBubbleDict.Add(pivotPoint, null);
                }
            }

            _reviewCountdown = BubbleAppearTimeout;
        }

        public override void Tick(float deltaTime)
        {
            if (!_isActive)
            {
                return;
            }

            if (_reviewCountdown >= 0f)
            {
                _reviewCountdown -= deltaTime;
                if (_reviewCountdown < 0)
                {
                    ShowBubbleOverRandomPivot();

                    _reviewCountdown = BubbleAppearTimeout;
                }
            }

            foreach (Transform pivotTransform in _listererToBubbleDict.Keys.ToList())
            {
                _listererToBubbleDict.TryGetValue(pivotTransform, out var bubbleData);
                if (bubbleData == null)
                {
                    continue;
                }

                if (bubbleData.TimeLeft >= 0)
                {
                    bubbleData.TimeLeft -= deltaTime;
                    if (bubbleData.TimeLeft < 0)
                    {
                        OnBubbleFaded(pivotTransform);
                    }
                }
            }
        }

        private void ShowBubbleOverRandomPivot()
        {
            var emptyPivots = _listererToBubbleDict.Where(pair => pair.Value == null).Select(pair => pair.Key)
                .ToList();
            if (emptyPivots.Count == 0)
            {
                return;
            }

            bool isPositive = Random.Range(0, 2) == 1;

            var reviewsList = isPositive ? _audienceReviews.PositiveReviews : _audienceReviews.NegativeReviews;
            Review review = reviewsList[Random.Range(0, reviewsList.Count)];

            float fadeDurationSec =
                isPositive ? _gameParams.PositiveBubbleFadeTimeSec : _gameParams.NegativeBubbleFadeTimeSec;

            Transform randomPivotPoint = emptyPivots[Random.Range(0, emptyPivots.Count)];
            BubbleWidget bubbleWidget = _uiView.CreateBubble(randomPivotPoint, review.Text, isPositive, OnBubbleClick);
            _listererToBubbleDict[randomPivotPoint] = new BubbleData(bubbleWidget, fadeDurationSec, isPositive);
        }

        private void OnBubbleFaded(Transform pivotTransform)
        {
            _listererToBubbleDict.TryGetValue(pivotTransform, out BubbleData bubbleData);
            if (bubbleData == null)
            {
                Debug.LogError("Data for transform not found");
                return;
            }

            if (bubbleData.IsPositive)
            {
                _impressionModel.ImpressionLevel += _gameParams.PositiveFadePointsGain;
            }
            else
            {
                _impressionModel.ImpressionLevel -= _gameParams.NegativeFadePointsLoss;
            }

            _uiView.HideBubble(bubbleData.Widget);
            _listererToBubbleDict[pivotTransform] = null;
        }

        private void OnBubbleClick(BubbleWidget bubbleWidget)
        {
            if (bubbleWidget.IsPositive)
            {
                _impressionModel.ImpressionLevel -= _gameParams.PositiveClickPointsLoss;
            }
            else
            {
                _impressionModel.ImpressionLevel += _gameParams.NegativeClickPointsGain;
            }

            _listererToBubbleDict[bubbleWidget.PivotTransform] = null;
            _uiView.HideBubble(bubbleWidget);
        }

        public void SetActive(bool value)
        {
            _isActive = value;
        }

        public void RemoveAllBubbles()
        {
            foreach (var bubbleData in _listererToBubbleDict.Values.Where(widget => widget != null).ToList())
            {
                _uiView.HideBubble(bubbleData.Widget);
            }

            foreach (Transform key in _listererToBubbleDict.Keys.ToList())
            {
                _listererToBubbleDict[key] = null;
            }
        }

        public override void Dispose()
        {
            RemoveAllBubbles();

            _listererToBubbleDict.Clear();
        }
    }
}