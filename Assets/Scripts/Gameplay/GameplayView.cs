using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class GameplayView : BaseView
    {
        [SerializeField] private GameParams _gameParams = null;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GameplayUiView _uiView = null;
        [SerializeField] private BandView _bandView = null;
        [SerializeField] private AudienceView _audienceView = null;
        [SerializeField] private EnvironmentView _environmentView = null;

        public GameParams GameParams => _gameParams;
        public GameplayUiView UiView => _uiView;
        public EnvironmentView EnvironmentView => _environmentView;

        public void Initialize(GameplayModel model, Timer timer)
        {
            _bandView.Initialize(model);
            _audienceView.Initialize(_uiView);
            _uiView.Initialize(_mainCamera, model.ImpressionModel, timer, model.BandList);
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