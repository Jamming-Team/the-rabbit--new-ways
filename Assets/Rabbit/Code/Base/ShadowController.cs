using System;
using UnityEngine;

namespace Rabbit
{
    [RequireComponent(typeof(ShadowPhaseManager))]
    public class ShadowController : MonoBehaviour
    {
        private ShadowPhaseManager _phaseManager;
        
        public event Action<int> OnPhaseChanged; 
        public event Action OnMaxGrowthReached;


        private void Awake()
        {
            _phaseManager = GetComponent<ShadowPhaseManager>();
            
            if (_phaseManager == null)
            {
                Debug.LogError($"ShadowPhaseManager is required on {gameObject.name}", this);
            }
        }

        private void Start()
        {
            // START for tests
            // StartShadowGrowth();
            // END for tests
            
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
            Debug.Log($"Phase changed to {phase}");
            
            OnPhaseChanged?.Invoke(phase);
            UpdateShadowVisual(phase);
        }

        private void HandleMaxGrowthReached()
        {
            Debug.Log("HandleMaxGrowthReached");
            
            OnMaxGrowthReached?.Invoke();
        }
        
        private void UpdateShadowVisual(int phase)
        {
            // TODO: Заменить спрайт тени на более большой
        }
    }
}