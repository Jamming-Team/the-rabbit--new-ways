using System;
using UnityEngine;

namespace Rabbit
{
    public class ShadowPhaseManager : MonoBehaviour
    {
        [SerializeField] private float _phaseDuration = 5f;
        [SerializeField] private int _maxPhases = 3;

        [SerializeField] SoundData _running;
        [SerializeField] SoundData _exiting;

        private int _currentPhase = 0;
        private float _phaseTimer = 0f;
        private bool _isGrowing = false;

        public event Action<int> OnPhaseChanged;
        public event Action OnMaxGrowthReached;

        SoundEmitter _runningEmitter;

        void Awake() {
            GameEvents.Gameplay.OnGameplayUpdate += OnGameplayUpdate;
            // DeactivateShadow();
        }
        
        void OnDestroy() {
            GameEvents.Gameplay.OnGameplayUpdate -= OnGameplayUpdate;
        }

        void OnGameplayUpdate(float obj) {
            
            if (!_isGrowing)
                return;

            _phaseTimer += Time.deltaTime;

            if (_phaseTimer >= _phaseDuration)
            {
                MoveToNextPhase();
                _phaseTimer = 0f;
            }
        }

        public void StartGrowth()
        {
            if (_isGrowing) return;

            _runningEmitter = AudioManager.Instance.PlaySound(_running, transform);
            
            ResetGrowth();
            _isGrowing = true;

            MoveToNextPhase();
        }

        public void DeactivateShadow()
        {
            if (_runningEmitter) {
                _runningEmitter.Stop();
                _runningEmitter = null;
            }
            
            AudioManager.Instance.PlaySound(_exiting, transform);
            
            ResetGrowth();
            _isGrowing = false;
            MoveToNextPhase();
        }

        // private void Update()
        // {
        //     if (!_isGrowing)
        //         return;
        //
        //     _phaseTimer += Time.deltaTime;
        //
        //     if (_phaseTimer >= _phaseDuration)
        //     {
        //         MoveToNextPhase();
        //         _phaseTimer = 0f;
        //     }
        // }

        private void MoveToNextPhase() {
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
            _currentPhase = -1;
            _phaseTimer = 0f;

        }
    }
}
