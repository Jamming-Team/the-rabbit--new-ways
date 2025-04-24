using UnityEngine;

namespace Rabbit {
    public class GamePreloader : MonoBehaviour {
        void Start() {
            GameManager.Instance.RequestSceneLoad(GC.Scenes.MAIN_MENU);
        }
    }
}