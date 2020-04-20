using System.Linq;
using TMPro.EditorUtilities;
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
                throw new System.NotImplementedException();
            }

            public override void Tick(float deltaTime)
            {
                throw new System.NotImplementedException();
            }

            public override void TogglePauseAction()
            {
                throw new System.NotImplementedException();
            }

            public override void FinishGameAction(GameStateChangeReason reason)
            {
                throw new System.NotImplementedException();
            }

            public override void Dispose()
            {
                throw new System.NotImplementedException();
            }
        }

        public class RunningGameState : GameState
        {
            public override void Initialize()
            {
                _context.Timer.Unpause();
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
                int musiciansOnStageCount = _context.Model.BandList
                    .Where(musicianModel => musicianModel.StageState == StageState.OnStage).ToList().Count;
                _context.Model.ImpressionModel.ImpressionLevel +=
                    musiciansOnStageCount * _context.GameplayController.GameParams.ImpressionGainPerMusicianSpeed;
            }

            public override void Dispose()
            {
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
                // TODO: Open game finished dialog
            }

            public override void TogglePauseAction()
            {
            }

            public override void FinishGameAction(GameStateChangeReason reason)
            {
            }

            public override void Tick(float deltaTime)
            {
            }

            public override void Dispose()
            {
            }
        }
    }
}