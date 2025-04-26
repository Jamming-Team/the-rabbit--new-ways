namespace Rabbit.Gameplay.InterfaceState {
    public class PauseState : SceneState<InterfaceState> {
        
        protected override void OnEnter() {
            base.OnEnter();

            GameEvents.UI.OnButtonPressed += OnButtonPressed;
            
        }

        protected override void OnExit() {
            base.OnExit();
            GameEvents.UI.OnButtonPressed -= OnButtonPressed;
        }
        
        void OnButtonPressed(GC.UI.ButtonTypes obj) {

            switch (obj) {
                case GC.UI.ButtonTypes.Quit:
                    GameManager.Instance.RequestSceneLoad(GC.Scenes.MAIN_MENU, true);
                    break;
            }
            
        }
        
    }
}