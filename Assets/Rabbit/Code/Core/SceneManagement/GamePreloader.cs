using UnityEngine;

namespace Rabbit
{
    public class GamePreloader : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.RequestSceneLoad(GC.Scenes.MAIN_MENU, true);
        }
    }
}