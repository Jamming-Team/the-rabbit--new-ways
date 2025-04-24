using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rabbit.UI {
    [RequireComponent(typeof(Button))]
    public class ButtonTypeSelector : MonoBehaviour {
        public GC.UI.ButtonTypes type;
        [HideInInspector] public Button reference;

        void Awake() {
            reference = GetComponent<Button>();
        }
    }
}