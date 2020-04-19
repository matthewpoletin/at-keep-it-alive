using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class AudienceView : BaseView
    {
        [SerializeField] private List<TableView> _tablesContainer = null;
        [SerializeField] private AudienceReviews _audienceReviews = null;

        private GameplayUiView _uiView;

        private const float BubbleAppearTimeout = 2.5f;
        
        private readonly Dictionary<Transform, BubbleWidget>
            _audiencePivots = new Dictionary<Transform, BubbleWidget>();

        private float _negativeReviewsCountdown = 0f;

        public void Initialize(GameplayUiView uiView)
        {
            _uiView = uiView;

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

            _negativeReviewsCountdown = BubbleAppearTimeout;
        }

        public override void Tick(float deltaTime)
        {
            if (_negativeReviewsCountdown >= 0f)
            {
                _negativeReviewsCountdown -= deltaTime;
                if (_negativeReviewsCountdown < 0)
                {
                    Review negativeReview =
                        _audienceReviews.NegativeReviews[Random.Range(0, _audienceReviews.NegativeReviews.Count)];
                    ShowBubbleOverRandomPivot(negativeReview.Text);

                    _negativeReviewsCountdown = BubbleAppearTimeout;
                }
            }
        }

        private void ShowBubbleOverRandomPivot(string text)
        {
            var emptyPivots = _audiencePivots.Where(pair => pair.Value == null).Select(pair => pair.Key).ToList();
            if (emptyPivots.Count == 0)
            {
                return;
            }

            Transform randomPivotPoint = emptyPivots[Random.Range(0, emptyPivots.Count)];
            _audiencePivots[randomPivotPoint] = _uiView.CreateBubble(randomPivotPoint, text, OnNegativeBubbleClick);
        }

        public void OnNegativeBubbleClick(BubbleWidget bubbleWidget)
        {
            var resultingPair = _audiencePivots.FirstOrDefault(pair => pair.Value == bubbleWidget);
            _uiView.HideBubble(resultingPair.Value);
            _audiencePivots[resultingPair.Key] = null;
            // TODO: Add points
        }

        public override void Dispose()
        {
            foreach (var audiencePivot in _audiencePivots)
            {
                _uiView.HideBubble(audiencePivot.Value);
            }

            _audiencePivots.Clear();
        }
    }
}