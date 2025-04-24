using UnityEngine;

namespace Rabbit
{
    public class GameManager :Singleton<GameManager>
    {
        [SerializeField] SceneLoader _sceneLoader;

        public void RequestSceneLoad(string sceneName, bool withAnims = false)
        {
            _sceneLoader.TryLoadScene(sceneName, withAnims);
        }
    }
}