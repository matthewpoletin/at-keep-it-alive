using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class GameplayView : BaseView
    {
        [SerializeField] private GameParams _gameParams = null;
        [SerializeField] private Camera _mainCamera = null;
        [SerializeField] private GameplayUiView _uiView = null;
        [SerializeField] private BandView _bandView = null;
        [SerializeField] private AudienceView _audienceView = null;
        [SerializeField] private EnvironmentView _environmentView = null;
        [SerializeField] private AudioManager _audioManager = null;

        public GameParams GameParams => _gameParams;
        public GameplayUiView UiView => _uiView;
        public BandView BandView => _bandView;
        public AudienceView AudienceView => _audienceView;
        public EnvironmentView EnvironmentView => _environmentView;
        public AudioManager AudioManager => _audioManager;

        public void Initialize(GameplayModel model, Timer timer)
        {
            _bandView.Initialize(model);
            _audienceView.Initialize(_uiView, model.ImpressionModel, _gameParams);
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