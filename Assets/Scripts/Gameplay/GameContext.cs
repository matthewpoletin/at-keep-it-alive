namespace KnowCrow.AT.KeepItAlive
{
    public class GameContext : LifecycleItem
    {
        private GameState _currentState = null;

        public GameplayUiView UiView { get; private set; }
        public GameplayModel Model { get; private set; }

        public Timer Timer { get; private set; }

        public GameContext(GameplayUiView uiView, GameplayModel model)
        {
            UiView = uiView;
            Model = model;

            Timer = new Timer(10);
            Timer.Pause();
        }

        public void ChangeState(GameState state)
        {
            _currentState?.Dispose();

            _currentState = state;
            _currentState.SetContext(this);
            _currentState.Initialize();
        }

        public void GameFinished(GameStateChangeReason reason)
        {
            _currentState.FinishGameAction(reason);
        }

        public override void Tick(float deltaTime)
        {
            _currentState.Tick(deltaTime);
            Timer.Tick(deltaTime);
        }

        public override void Dispose()
        {
            _currentState.Dispose();
        }
    }
}