using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Rabbit
{
    [RequireComponent(typeof(ShadowPhaseManager))]
    public class ShadowController : MonoBehaviour {
        [FormerlySerializedAs("_sprite")] [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] List<Sprite> _sprites; 
        
        private ShadowPhaseManager _phaseManager;
        
        public event Action<int> OnPhaseChanged; 
        public event Action OnMaxGrowthReached;
        public event Action<ShadowController> OnStoppedGrowth;

        public event Action<ShadowController, bool> OnSetCanGrow;


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
            StopShadowGrowth();
        }

        private void OnDestroy()
        {
            if (_phaseManager == null) return;
                    
            _phaseManager.OnPhaseChanged -= HandlePhaseChanged;
            _phaseManager.OnMaxGrowthReached -= HandleMaxGrowthReached;
        }

        public void StartShadowGrowth()
        {
            _spriteRenderer.color = Color.white;
            _phaseManager.StartGrowth();
        }

        public void StopShadowGrowth()
        {
            _spriteRenderer.color = Color.clear;
            _phaseManager.DeactivateShadow();
            OnStoppedGrowth?.Invoke(this);
        }

        public void SetCanGrow(bool flag) {
            OnSetCanGrow?.Invoke(this, flag);
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
            _spriteRenderer.sprite = _sprites[phase];
            // TODO: Заменить спрайт тени на более большой
        }
        
    }
}