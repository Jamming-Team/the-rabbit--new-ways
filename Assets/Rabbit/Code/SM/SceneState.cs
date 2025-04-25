using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rabbit {
    public abstract class SceneState<TContextType> : State<TContextType> where TContextType : MonoBehaviour {
        [SerializeField] protected List<GameObject> _views;

        public override void Init(MonoBehaviour context) {
            base.Init(context);
            SetViewsVisibility(false);
        }

        protected override void OnEnter() {
            SetViewsVisibility(true);
            GameEvents.UI.OnButtonPressed += OnUIButtonPressed;
        }

        protected override void OnExit() {
            SetViewsVisibility(false);
            GameEvents.UI.OnButtonPressed -= OnUIButtonPressed;
        }

        void SetViewsVisibility(bool visibility) {
            _views?.ForEach(x => {
                // Debug.Log(x.gameObject.name);
                if (x)
                    x.SetActive(visibility);
            });
        }
        
        protected virtual void OnUIButtonPressed(GC.UI.ButtonTypes type) {}
    }
}