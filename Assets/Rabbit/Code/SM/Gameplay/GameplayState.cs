namespace Rabbit.Gameplay {
    public class GameplayState : State<ActionState> {

        float timer;

        protected override void OnEnter() {
            base.OnEnter();

            timer = 0;
        }
        
        protected override void OnExit() {
            base.OnExit();
            
            
        }

        public override void UpdateState(float delta) {
            _core.currentTime += delta;
            timer += delta;

            if (timer >= ((GameplayStateData)_core.stateData).timeTillNextAction) {
                _core.DecideOnNextAction_State();
            }
        }
        
    }
}