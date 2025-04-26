using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rabbit
{
    [RequireComponent(typeof(ShadowPhaseManager))]
    public class ShadowController : MonoBehaviour {
        [SerializeField] SpriteRenderer _sprite;
        [SerializeField] List<Sprite> _sprites; 
        
        private ShadowPhaseManager _phaseManager;
        
        public event Action<int> OnPhaseChanged; 
        public event Action OnMaxGrowthReached;
        public event Action<ShadowController> OnStoppedGrowth;


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
            OnStoppedGrowth?.Invoke(this);
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
            
            GameEvents.Gameplay.MaxGrowthReached?.Invoke();
        }
        
        private void UpdateShadowVisual(int phase) {
            _sprite.sprite = _sprites[phase];
            // TODO: Заменить спрайт тени на более большой
        }
    }
}