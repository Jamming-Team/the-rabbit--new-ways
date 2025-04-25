using UnityEngine;

namespace Rabbit {
    public class GP_SceneController: MonoBehaviour {
        [SerializeField] StateMachine _stateMachine;

        [HideInInspector] public ContentData data;
        
        protected virtual void Start() {
            _stateMachine.Init(this, false);
        }
    }
}