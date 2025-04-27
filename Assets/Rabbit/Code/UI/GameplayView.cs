using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rabbit.UI {
    public class GameplayView : MonoBehaviour {

        [SerializeField] List<GameObject> _batteries;


        void Start() {
            GameEvents.Gameplay.OnBatteryCountChanged += OnBatteryCountChanged;
            // HideAll();
        }

        void OnDestroy() {
            GameEvents.Gameplay.OnBatteryCountChanged -= OnBatteryCountChanged;
        }

        void OnBatteryCountChanged(int obj) {
            HideAll();

            for (int i = 0; i < obj; i++) {
                _batteries[i].SetActive(true);
            }
            

        }


        void HideAll() {
            _batteries.ForEach(x => x.SetActive(false));
        }

    }
}