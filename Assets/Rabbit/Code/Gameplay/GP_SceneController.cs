using System;
using Rabbit.Gameplay;
using UnityEngine;
// using Rabbit.Gameplay.InterfaceState;

namespace Rabbit {
    public class GP_SceneController: MonoBehaviour, IVisitable {
        [SerializeField] StateMachine _stateMachine;

        [HideInInspector] public ContentData data;
        
        [HideInInspector]
        public Type interfaceType;
        
        protected virtual void Start() {
            GameManager.Instance.RequestData(this);
            data.currentBlockNum = 0;
            _stateMachine.Init(this, false);
            
            _stateMachine.ChangeState(typeof(ActionState));
        }

        void Update() {
            _stateMachine.UpdateSM(Time.deltaTime);
        }

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }
    }
}