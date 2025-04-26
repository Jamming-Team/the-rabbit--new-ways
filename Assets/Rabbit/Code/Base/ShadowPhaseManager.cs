using System;
using UnityEngine;

namespace Rabbit
{
    public class ShadowPhaseManager : MonoBehaviour
    {
        [SerializeField] private float _phaseDuration = 5f;
        [SerializeField] private int _maxPhases = 3;

        private int _currentPhase = 0;
        private float _phaseTimer = 0f;
        private bool _isGrowing = false;

        public event Action<int> OnPhaseChanged;
        public event Action OnMaxGrowthReached;
        
        public void StartGrowth()
        {
            if (_isGrowing) return; 
            
            ResetGrowth();
            _isGrowing = true;
        }

        public void DeactivateShadow()
        {
            _isGrowing = false;
        }

        private void Update()
        {
            if (!_isGrowing)
                return;

            _phaseTimer += Time.deltaTime;

            if (_phaseTimer >= _phaseDuration)
            {
                MoveToNextPhase();
                _phaseTimer = 0f;
            }
        }

        private void MoveToNextPhase()
        {
            _currentPhase++;

            if (_currentPhase >= _maxPhases)
            {
                _currentPhase = _maxPhases;
                DeactivateShadow();
                OnMaxGrowthReached?.Invoke();
                return;
            }

            OnPhaseChanged?.Invoke(_currentPhase);
        }

        private void ResetGrowth()
        {
            _currentPhase = 0;
            _phaseTimer = 0f;
        }
    }
}
