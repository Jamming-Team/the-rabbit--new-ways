using System;
using UnityEngine;

namespace Rabbit {
    public class GP_SceneController: MonoBehaviour, IVisitable {
        [SerializeField] StateMachine _stateMachine;

        [HideInInspector] public ContentData data;
        
        protected virtual void Start() {
            GameManager.Instance.RequestData(this);
            _stateMachine.Init(this, false);
        }

        void Update() {
            _stateMachine.UpdateSM(Time.deltaTime);
        }

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }
    }
}