using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class BandView : BaseView
    {
        [SerializeField] private List<MusicianView> _musicians = null;
        [SerializeField] private List<MusicianPlayingSpot> _playingSpot = null;
        [SerializeField] private Transform _offStageContainer = null;

        private readonly List<Transform> _offStageSpots = new List<Transform>();

        public void Initialize(GameplayModel model)
        {
            for (int i = 0; i < _offStageContainer.childCount; i++)
            {
                _offStageSpots.Add(_offStageContainer.GetChild(i));
            }

            if (_offStageSpots.Count == 0)
            {
                Debug.LogError("No off stage spots found");
            }

            foreach (MusicianView musicianView in _musicians)
            {
                var musicianModel = new MusicianModel(musicianView.MusicianType, musicianView.MusicianData);
                model.AddMusician(musicianModel);
                MusicianPlayingSpot musicianSpot =
                    _playingSpot.FirstOrDefault(spot => spot.MusicianType == musicianView.MusicianType);
                if (musicianSpot == null)
                {
                    Debug.LogError($"Musician spot of type '{musicianView.MusicianType}' not found");
                }

                musicianView.Initialize(musicianModel, musicianSpot, this);
                Transform randomOffStageSpot = GetRandomOffStageSpot();
                musicianView.transform.position = randomOffStageSpot.position;
            }
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            foreach (MusicianView musicianView in _musicians)
            {
                musicianView.Tick(deltaTime);
            }
        }

        public Transform GetRandomOffStageSpot()
        {
            return _offStageSpots[Random.Range(0, _offStageSpots.Count)];
        }
    }
}