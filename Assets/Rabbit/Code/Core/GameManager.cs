using UnityEngine;

namespace Rabbit
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] SceneLoader _sceneLoader;
        [SerializeField] GameDataSO _dataSO;
        [SerializeField] InputReader _inputReader;
        
        public InputReader inputReader => _inputReader;
        
        DataManager _dataManager;

        protected override void Awake() {
            base.Awake();
            Application.targetFrameRate = 60;
            _dataManager = new DataManager(_dataSO);
            _inputReader.EnablePlayerActions();
        }

        public void RequestSceneLoad(string sceneName, bool withAnims = false)
        {
            _sceneLoader.TryLoadScene(sceneName, withAnims);
        }
        
        public void RequestData(IVisitable requester) {
            _dataManager.TrySupply(requester);
        }
        

    }
}