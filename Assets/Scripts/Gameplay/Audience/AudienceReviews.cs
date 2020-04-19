using System;
using System.Collections.Generic;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    [Serializable]
    public class Review
    {
        [SerializeField] private string _text;

        public string Text => _text;
    }

    [CreateAssetMenu(fileName = "AudienceReview", menuName = "Params/AudienceReview", order = 0)]
    public class AudienceReviews : ScriptableObject
    {
        [SerializeField] private List<Review> _positiveReviews;
        [SerializeField] private List<Review> _negativeReviews;

        public List<Review> PositiveReviews => _positiveReviews;
        public List<Review> NegativeReviews => _negativeReviews;
    }
}