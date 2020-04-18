using System.Collections.Generic;

namespace KnowCrow.AT.KeepItAlive
{
    public class GameplayModel
    {
        public ImpressionModel ImpressionModel;
        public List<MusicianModel> BandList;

        public GameplayModel()
        {
            BandList = new List<MusicianModel>();
            ImpressionModel = new ImpressionModel();
        }

        public void AddMusician(MusicianModel musicianModel)
        {
            BandList.Add(musicianModel);
        }
    }
}