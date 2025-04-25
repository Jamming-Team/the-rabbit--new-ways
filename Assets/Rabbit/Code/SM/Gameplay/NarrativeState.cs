namespace Rabbit.Gameplay {
    public class NarrativeState : State<ActionState> {
        protected override void OnEnter() {
            base.OnEnter();
            GameEvents.Narrative.OnStartNarrative?.Invoke((_core.stateData as NarrativeStateData).narrative);
            GameEvents.Narrative.OnEndNarrative += OnEndNarrative;
        }

        protected override void OnExit() {
            base.OnExit();
            GameEvents.Narrative.OnEndNarrative -= OnEndNarrative;
        }

        void OnEndNarrative() {
            _core.DecideOnNextAction_State();
        }
    }
}