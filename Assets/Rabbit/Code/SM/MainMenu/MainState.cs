using UnityEngine;

namespace Rabbit.MainMenu {
    public class MainState : SceneState<MM_SceneController> {
        protected override void OnUIButtonPressed(GC.UI.ButtonTypes type) {
            // Debug.Log("OnUIButtonPressed : " + type);
            switch (type) {
                case GC.UI.ButtonTypes.Play: {
                    GameManager.Instance.RequestSceneLoad(GC.Scenes.GAMEPLAY, true);
                    break;
                }
                case GC.UI.ButtonTypes.Settings: {
                    RequestTransition<SettingsState>();
                    break;
                }
            }
        }
    }
}