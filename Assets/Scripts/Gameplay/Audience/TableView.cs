using System.Collections.Generic;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class TableView : MonoBehaviour
    {
        [SerializeField] private List<Transform> _messagePivotPoint = null;

        public List<Transform> MessagePivotPoint => _messagePivotPoint;
    }
}