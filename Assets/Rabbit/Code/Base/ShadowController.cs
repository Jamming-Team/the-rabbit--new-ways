using System;
using UnityEngine;

namespace Rabbit
{
    public class ShadowController : MonoBehaviour
    {
        [SerializeField] private GameObject _shadowObject;
        [SerializeField] private float _phaseDuration = 5f;

        private const int MaxPhases = 3;

        private int _currentPhase = 0;
        private float _phaseTimer = 0f;
        private bool _isGrowing = false;

        public event Action<int> OnPhaseChanged;
        public event Action OnMaxGrowthReached;
        
        public void StartGrowth()
        {
            ResetGrowth();
            _isGrowing = true;
        }

        public void DeactivateShadow()
        {
            _isGrowing = false;
        }

        // START for tests
        // private void Start()
        // {
        //     StartGrowth();
        // }
        // END for tests

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

            OnPhaseChanged?.Invoke(_currentPhase);

            if (_currentPhase >= MaxPhases)
            {
                DeactivateShadow();
                OnMaxGrowthReached?.Invoke();
            }
        }

        private void ResetGrowth()
        {
            _currentPhase = 0;
            _phaseTimer = 0f;
        }
    }
}