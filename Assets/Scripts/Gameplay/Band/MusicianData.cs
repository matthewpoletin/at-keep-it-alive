using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    [CreateAssetMenu(fileName = "MusicianData", menuName = "Params/MusicianData", order = 0)]
    public class MusicianData : ScriptableObject
    {
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _manaLossSpeed = 1f;
        [SerializeField] private float _manaGainSpeed = 1f;

        public float MovementSpeed => _movementSpeed;
        public float ManaLossSpeed => _manaLossSpeed;
        public float ManaGainSpeed => _manaGainSpeed;
    }
}