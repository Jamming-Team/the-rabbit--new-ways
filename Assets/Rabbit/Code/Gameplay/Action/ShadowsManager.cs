using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rabbit {
    public class ShadowsManager : MonoBehaviour {

        [SerializeField] List<ShadowController> _shadows;
        List<ShadowController> _availableShadows = new List<ShadowController>();

        DifficultySet _difficultySet;

        int _currentShadows = 0;
        float _spawnTimer = 0;
        
        void Awake() {
            GameEvents.Gameplay.OnChangeDifficultySet += OnChangeDifficultySet;
            GameEvents.Gameplay.OnGameplayUpdate += OnGameplayUpdate;
        }

        void Start() {
            _shadows.ForEach(x => {
                _availableShadows.Add(x);
                x.OnStoppedGrowth += OnOnStoppedGrowth;
            });
        }

        void OnDestroy() {
            GameEvents.Gameplay.OnChangeDifficultySet -= OnChangeDifficultySet;
            GameEvents.Gameplay.OnGameplayUpdate += OnGameplayUpdate;
            _shadows.ForEach(x => {
                x.OnStoppedGrowth -= OnOnStoppedGrowth;
            });
        }
        
        
        void OnChangeDifficultySet(DifficultySet obj) {
            _difficultySet = obj;
        }
        
        void OnGameplayUpdate(float obj) {
            _spawnTimer += obj;

            if (_spawnTimer >= _difficultySet.timeTillNextShadow
                && _currentShadows < _difficultySet.maxShadows) {
                var randomShadowIndex = Random.Range(0, _availableShadows.Count);
                _availableShadows[randomShadowIndex].StartShadowGrowth();
                _availableShadows.RemoveAt(randomShadowIndex);
                _currentShadows++;
                _spawnTimer = 0f;
            }
            
        }
        
        void OnOnStoppedGrowth(ShadowController shadow) {
            if (!_availableShadows.Contains(shadow))
                return;
            
            _availableShadows.Add(shadow);
            _currentShadows--;
        }
    }
}