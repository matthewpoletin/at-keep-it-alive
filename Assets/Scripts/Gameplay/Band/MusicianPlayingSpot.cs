using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class MusicianPlayingSpot : BaseView
    {
        [SerializeField] private MusicianType _musicianType = default;

        public MusicianType MusicianType => _musicianType;
    }
}