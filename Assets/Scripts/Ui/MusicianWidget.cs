using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class MusicianWidget : BaseView
    {
        [SerializeField] private List<MusicianWidgetItemView> _musicianWidgets = null;

        public void Initialize(List<MusicianModel> musicians)
        {
            foreach (MusicianWidgetItemView musicianWidget in _musicianWidgets)
            {
                MusicianModel musicianModel =
                    musicians.FirstOrDefault(model => model.MusicianType == musicianWidget.MusicianType);
                if (musicianModel == null)
                {
                    Debug.LogError("MusicianModel not found");
                }

                musicianWidget.Initialize(musicianModel);
            }
        }
    }
}