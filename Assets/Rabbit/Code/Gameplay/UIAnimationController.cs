using UnityEngine;

namespace Rabbit {
    public class UIAnimationController : NarrativeBase {

        [SerializeField] Animator _animator;

        public void PlayAnimation(string triggerName) {
            _animator.SetTrigger(triggerName);
        }

        public void CallEndNarrative() {
            NarrativeEnded?.Invoke();
        }

    }
}