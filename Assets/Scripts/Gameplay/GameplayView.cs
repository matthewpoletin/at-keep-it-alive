using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class GameplayView : BaseView
    {
        [SerializeField] private GameplayUiView _uiView = null;
        [SerializeField] private BandView _bandView = null;
        [SerializeField] private AudienceView _audienceView = null;

        public GameplayUiView UiView => _uiView;

        public void Initialize(GameplayModel model, Timer timer)
        {
            _bandView.Initialize(model);
            _audienceView.Initialize();
            _uiView.Initialize(model.ImpressionModel, timer, model.BandList);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            _bandView.Tick(deltaTime);
            _audienceView.Tick(deltaTime);
            _uiView.Tick(deltaTime);
        }

        public override void Dispose()
        {
            _uiView.Dispose();
            _bandView.Dispose();
            _audienceView.Dispose();
        }
    }
}