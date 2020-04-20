using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class GameplayController : LifecycleItem
    {
        private GameplayView _view;
        private GameplayModel _model;

        public GameParams GameParams => _view.GameParams;
        public AudienceView AudienceView => _view.AudienceView;

        private readonly GameStateObserver _gameStateObserver;
        private readonly GameContext _gameContext;

        public GameplayController(GameplayView gameplayView)
        {
            _view = gameplayView;
            _model = new GameplayModel();

            _gameContext = new GameContext(this, _view.UiView, _view.EnvironmentView, _model);
            var initialGameState = new GameState.EntryGameState();
            _gameContext.ChangeState(initialGameState);

            _view.Initialize(_model, _gameContext.Timer);

            _gameStateObserver = new GameStateObserver(_model.ImpressionModel, _gameContext.Timer);
            _gameStateObserver.OnGameStateChanged += OnGameStateChanged;
        }

        public override void Tick(float deltaTime)
        {
            _gameContext.Tick(deltaTime);
            _view.Tick(deltaTime);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _gameContext.TogglePause();
            }
        }

        private void OnGameStateChanged(GameStateChangeReason reason)
        {
            _gameContext.GameFinished(reason);
        }

        public override void Dispose()
        {
            _gameStateObserver.OnGameStateChanged -= OnGameStateChanged;
            _view.Dispose();
        }
    }
}