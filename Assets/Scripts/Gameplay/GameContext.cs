namespace KnowCrow.AT.KeepItAlive
{
    public class GameContext : LifecycleItem
    {
        private GameState _currentState = null;

        private Timer _timer;

        public GameplayUiView UiView { get; private set; }

        public GameContext(GameplayUiView uiView)
        {
            UiView = uiView;
        }

        public Timer Timer { get; private set; }

        public void ChangeState(GameState state)
        {
            _currentState?.Dispose();

            _currentState = state;
            _currentState.SetContext(this);
        }

        public void GameFinished(GameStateChangeReason reason)
        {
            _currentState.FinishGameAction(reason);
        }

        public override void Tick(float deltaTime)
        {
            _currentState.Tick(deltaTime);
        }

        public override void Dispose()
        {
            _currentState.Dispose();
        }
    }
}