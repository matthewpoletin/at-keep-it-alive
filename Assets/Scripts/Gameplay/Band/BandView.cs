using System.Collections.Generic;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class BandView : BaseView
    {
        [SerializeField] private List<MusicianView> _musicians = null;

        public void Initialize(GameplayModel model)
        {
            foreach (MusicianView musicianView in _musicians)
            {
                var musicianModel = new MusicianModel(musicianView.MusicianType, musicianView.MusicianData);
                model.AddMusician(musicianModel);
                musicianView.Initialize(musicianModel);
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
    }
}