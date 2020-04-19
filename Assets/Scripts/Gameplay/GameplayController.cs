namespace KnowCrow.AT.KeepItAlive
{
    public class GameplayController : LifecycleItem
    {
        private GameplayView _view;
        private GameplayModel _model;

        private readonly GameStateObserver _gameStateObserver;
        private readonly GameContext _gameContext;

        public GameplayController(GameplayView gameplayView)
        {
            _view = gameplayView;
            _model = new GameplayModel();

            _gameContext = new GameContext(_view.UiView, _view.EnvironmentView, _model);
            var initialGameState = new GameState.EntryGameState();
            _gameContext.ChangeState(initialGameState);

            _view.Initialize(_model, _gameContext.Timer);

            _gameStateObserver = new GameStateObserver(_model.ImpressionModel);
            _gameStateObserver.OnGameStateChanged += OnGameStateChanged;
        }

        public override void Tick(float deltaTime)
        {
            _gameContext.Tick(deltaTime);
            _view.Tick(deltaTime);
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