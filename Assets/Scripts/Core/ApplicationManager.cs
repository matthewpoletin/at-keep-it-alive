using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private GameplayView _gameplayView = null;

        private GameplayController _gameplayController;
        
        private void Awake()
        {
            _gameplayController = new GameplayController(_gameplayView);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            _gameplayController.Tick(deltaTime);
        }
    }
}