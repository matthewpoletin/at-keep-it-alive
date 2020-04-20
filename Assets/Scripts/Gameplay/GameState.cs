using System.Linq;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public abstract class GameState : LifecycleItem
    {
        protected GameContext _context;

        public void SetContext(GameContext context)
        {
            _context = context;
        }

        public abstract void Initialize();
        public abstract void TogglePauseAction();
        public abstract void FinishGameAction(GameStateChangeReason reason);

        public class EntryGameState : GameState
        {
            public override void Initialize()
            {
                _context.UiView.ShowGameInfo();
            }

            public override void TogglePauseAction()
            {
                _context.ChangeState(new RunningGameState());
            }

            public override void FinishGameAction(GameStateChangeReason reason)
            {
            }

            public override void Tick(float deltaTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _context.UiView.HideGameInfo();
                    _context.ChangeState(new RunningGameState());
                }
            }

            public override void Dispose()
            {
            }
        }

        public class TutorialGameState : GameState
        {
            public TutorialGameState()
            {
            }

            public override void Initialize()
            {
            }

            public override void Tick(float deltaTime)
            {
            }

            public override void TogglePauseAction()
            {
            }

            public override void FinishGameAction(GameStateChangeReason reason)
            {
            }

            public override void Dispose()
            {
            }
        }

        public class RunningGameState : GameState
        {
            public override void Initialize()
            {
                _context.GameplayController.AudienceView.SetActive(true);
                _context.Timer.Unpause();
                _context.Model.ImpressionModel.ImpressionLevel = 1;
                _context.Timer.Reset(_context.GameplayController.GameParams.SessionDurationSec);
                _context.EnvironmentView.ShowShade();
            }

            public override void TogglePauseAction()
            {
                _context.ChangeState(new PausedGameState());
            }

            public override void FinishGameAction(GameStateChangeReason reason)
            {
                _context.ChangeState(new ShowResultGameState(reason));
            }

            public override void Tick(float deltaTime)
            {
                _context.Model.ImpressionModel.ImpressionLevel -=
                    _context.GameplayController.GameParams.ImpressionLossSpeed * deltaTime;
                int musiciansOnStageActive = _context.Model.BandList
                    .Where(musicianModel => musicianModel.StageState == StageState.OnStage && !musicianModel.IsTired).ToList().Count;
                int musiciansOnStageTired = _context.Model.BandList
                    .Where(musicianModel => musicianModel.StageState == StageState.OnStage && musicianModel.IsTired).ToList().Count;
                _context.Model.ImpressionModel.ImpressionLevel +=
                    musiciansOnStageActive * _context.GameplayController.GameParams.ImpressionGainPerMusicianSpeed * deltaTime;
                _context.Model.ImpressionModel.ImpressionLevel -=
                    musiciansOnStageTired * _context.GameplayController.GameParams.ImpressionLossPerMusicianTired * deltaTime;
            }

            public override void Dispose()
            {
                _context.GameplayController.AudienceView.SetActive(false);
                _context.Timer.Pause();
            }
        }

        public class PausedGameState : GameState
        {
            public PausedGameState()
            {
            }

            public override void Initialize()
            {
                _context.Timer.Pause();
                _context.EnvironmentView.HideFade(1f);
            }

            public override void TogglePauseAction()
            {
                _context.ChangeState(new RunningGameState());
            }

            public override void FinishGameAction(GameStateChangeReason reason)
            {
                _context.ChangeState(new ShowResultGameState(reason));
            }

            public override void Tick(float deltaTime)
            {
            }

            public override void Dispose()
            {
            }
        }

        public class ShowResultGameState : GameState
        {
            private GameStateChangeReason _reason;

            public ShowResultGameState(GameStateChangeReason reason)
            {
                _reason = reason;
            }

            public override void Initialize()
            {
                _context.GameplayController.AudienceView.RemoveAllBubbles();
                _context.UiView.HideAllScreens();
                switch (_reason)
                {
                    case GameStateChangeReason.GameLost:
                        _context.UiView.ShowDefeatScreen();
                        break;
                    case GameStateChangeReason.GameWon:
                        _context.UiView.ShowVictoryScreen();
                        break;
                }
            }

            public override void TogglePauseAction()
            {
            }

            public override void FinishGameAction(GameStateChangeReason reason)
            {
            }

            public override void Tick(float deltaTime)
            {
                if (Input.anyKeyDown)
                {
                    _context.ChangeState(new RunningGameState());
                }
            }

            public override void Dispose()
            {
                _context.UiView.HideAllScreens();
            }
        }
    }
}