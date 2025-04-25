using System.Collections;
using UnityEngine;

namespace Rabbit.Gameplay {
    public class NarrativeState : State<ActionState> {
        protected override void OnEnter() {
            base.OnEnter();
            GameEvents.Narrative.OnEndNarrative += OnEndNarrative;

            StartCoroutine(Narrate());
        }

        protected override void OnExit() {
            base.OnExit();
            GameEvents.Narrative.OnEndNarrative -= OnEndNarrative;
        }
        
        IEnumerator Narrate() {
            yield return new WaitForSeconds(0.1f);
            
            GameEvents.Narrative.OnStartNarrative?.Invoke((_core.stateData as NarrativeStateData).narrative);
        }

        void OnEndNarrative() {
            _core.DecideOnNextAction_State();
        }
    }
}