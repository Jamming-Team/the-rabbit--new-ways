using UnityEngine;

namespace Rabbit
{
    [RequireComponent(typeof(LightSource))]
    public class LightInteractor : MonoBehaviour
    {
        private LightSource _lightSource;

        private void Awake()
        {
            _lightSource = GetComponent<LightSource>();

            if (BatteryManager.Instance == null)
            {
                Debug.LogError("BatteryManager not found!", this);
            }
        }

        public void Interact()
        {
            if (_lightSource.IsOn)
            {
                _lightSource.TurnOff();
                BatteryManager.Instance.ReturnBattery();
                Debug.Log("Light turned OFF");
            }
            else if (BatteryManager.Instance.HasAvailableBattery())
            {
                BatteryManager.Instance.UseBattery();

                _lightSource.TurnOn();
                Debug.Log("Light turned ON");
            }
        }
    }
}