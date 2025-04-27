using UnityEngine;

namespace Rabbit
{
    // ---> Singleton <---
    public class BatteryManager : MonoBehaviour
    {
        public static BatteryManager Instance { get; private set; }

        [SerializeField] private int _availableBatteries = 1;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Singleton BatteryManager", this);
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public bool HasAvailableBattery()
        {
            GameEvents.Gameplay.OnBatteryCountChanged?.Invoke(_availableBatteries);
            
            return _availableBatteries > 0;
        }

        public void UseBattery()
        {
            if (HasAvailableBattery())
            {
                _availableBatteries--;
            }
            
            GameEvents.Gameplay.OnBatteryCountChanged?.Invoke(_availableBatteries);
        }

        public void ReturnBattery()
        {
            _availableBatteries++;
            
            GameEvents.Gameplay.OnBatteryCountChanged?.Invoke(_availableBatteries);
        }
    }
}