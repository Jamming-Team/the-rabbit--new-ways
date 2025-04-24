using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rabbit.UI {
    public class GeneralViewListener : MonoBehaviour {
        [SerializeField] List<ButtonTypeSelector> _buttons;

        void Start() {
            _buttons.ForEach(x => x.reference.onClick.AddListener(delegate { RaiseUIButtonPressedEvent(x.type); }));
        }

        void OnDestroy() {
            _buttons.ForEach(x => x.reference.onClick.RemoveListener(delegate { RaiseUIButtonPressedEvent(x.type); }));
        }

        void RaiseUIButtonPressedEvent(GC.UI.ButtonTypes type) {
            GameEvents.UI.OnButtonPressed?.Invoke(type);
        }
    }
}