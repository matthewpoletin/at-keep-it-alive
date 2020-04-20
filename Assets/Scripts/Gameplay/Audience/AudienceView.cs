﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class AudienceView : BaseView
    {
        [SerializeField] private List<TableView> _tablesContainer = null;
        [SerializeField] private AudienceReviews _audienceReviews = null;

        private GameplayUiView _uiView;
        private ImpressionModel _impressionModel;
        private GameParams _gameParams;

        private const float BubbleAppearTimeout = 2.5f;

        private readonly Dictionary<Transform, BubbleWidget>
            _audiencePivots = new Dictionary<Transform, BubbleWidget>();

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

                    _audiencePivots.Add(pivotPoint, null);
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
        }

        private void ShowBubbleOverRandomPivot()
        {
            var emptyPivots = _audiencePivots.Where(pair => pair.Value == null).Select(pair => pair.Key).ToList();
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
            BubbleWidget bubbleWidget = _uiView.CreateBubble(randomPivotPoint, review.Text, isPositive, fadeDurationSec,
                OnBubbleClick, OnBubbleFaded);
            _audiencePivots[randomPivotPoint] = bubbleWidget;
        }

        private void OnBubbleFaded(BubbleWidget bubbleWidget)
        {
            var resultingPair = _audiencePivots.FirstOrDefault(pair => pair.Value == bubbleWidget);
            _uiView.HideBubble(resultingPair.Value);
            _audiencePivots[resultingPair.Key] = null;
        }

        private void OnBubbleClick(BubbleWidget bubbleWidget)
        {
            var resultingPair = _audiencePivots.FirstOrDefault(pair => pair.Value == bubbleWidget);
            _uiView.HideBubble(resultingPair.Value);
            _audiencePivots[resultingPair.Key] = null;
            if (bubbleWidget.IsPositive)
            {
                _impressionModel.ImpressionLevel -= _gameParams.PositiveClickPointsLoss;
            }
            else
            {
                _impressionModel.ImpressionLevel += _gameParams.NegativeClickPointsGain;
            }
        }

        public void SetActive(bool value)
        {
            _isActive = value;
        }

        public void RemoveAllBubbles()
        {
            foreach (BubbleWidget bubble in _audiencePivots.Values.Where(widget => widget != null).ToList())
            {
                _uiView.HideBubble(bubble);
            }

            foreach (Transform key in _audiencePivots.Keys.ToList())
            {
                _audiencePivots[key] = null;
            }
        }

        public override void Dispose()
        {
            RemoveAllBubbles();

            _audiencePivots.Clear();
        }
    }
}