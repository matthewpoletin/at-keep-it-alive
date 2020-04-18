using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class MusicianView : BaseView
    {
        [SerializeField] private MusicianType _musicianType = default;
        [SerializeField] private Animator _animator = null;

        public MusicianType MusicianType => _musicianType;

        public void Initialize(MusicianModel musicianModel)
        {
        }
    }
}