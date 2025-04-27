using System;
using TMPro;
using UnityEngine;

namespace Rabbit.UI {
    public class PostGameView : MonoBehaviour {

        [SerializeField] public TMP_Text _text;


        void Start() {
            GameEvents.UI.OnSetPostGameText += OnSetPostGameText;
        }

        void OnDestroy() {
            GameEvents.UI.OnSetPostGameText -= OnSetPostGameText;
        }

        void OnSetPostGameText(string obj) {
            _text.text = obj;
        }
    }
}