using System;
using UnityEngine;

namespace Rabbit
{
    public class ShadowController : MonoBehaviour
    {
        [SerializeField] private GameObject _shadowObject;
        [SerializeField] private ShadowPhaseManager _phaseManager;
        
        public event Action<int> OnPhaseChanged; 
        public event Action OnMaxGrowthReached;

        private void Start()
        {
            _phaseManager.OnPhaseChanged += HandlePhaseChanged;
            _phaseManager.OnMaxGrowthReached += HandleMaxGrowthReached;
        }

        private void OnDestroy()
        {
            if (_phaseManager == null) return;
                    
            _phaseManager.OnPhaseChanged -= HandlePhaseChanged;
            _phaseManager.OnMaxGrowthReached -= HandleMaxGrowthReached;
        }

        public void StartShadowGrowth()
        {
            _phaseManager.StartGrowth();
        }

        public void StopShadowGrowth()
        {
            _phaseManager.DeactivateShadow();
        }

        private void HandlePhaseChanged(int phase)
        {
            OnPhaseChanged?.Invoke(phase);
            UpdateShadowVisual(phase);
        }

        private void HandleMaxGrowthReached()
        {
            OnMaxGrowthReached?.Invoke();
        }
        
        private void UpdateShadowVisual(int phase)
        {
            // TODO: Заменить спрайт тени на более большой
        }
    }
}