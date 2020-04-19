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
        public abstract void ActivateAction();
        public abstract void FinishGameAction(GameStateChangeReason reason);

        public class EntryGameState : GameState
        {
            public override void Initialize()
            {
                _context.UiView.ShowGameInfo();
            }

            public override void ActivateAction()
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

        public class RunningGameState : GameState
        {
            public override void Initialize()
            {
                _context.Timer.Unpause();
                _context.EnvironmentView.ShowShade();
            }

            public override void ActivateAction()
            {
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

            public override void ActivateAction()
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

            public override void ActivateAction()
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