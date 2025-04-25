using System;
using UnityEngine;

namespace Rabbit {
    public class NarrativeController : MonoBehaviour {

        [SerializeField] GameObject _comicsRoot;
        [SerializeField] GameObject _dialogueRoot;
        [SerializeField] UIAnimationController _animationController;

        NarrativeBase _curBase;
        
        void Start() {
            GameEvents.Narrative.OnStartNarrative += OnStartNarrative;
            _animationController.NarrativeEnded += HandleNarrativeEnd;
        }

        void OnDestroy() {
            if (_curBase) {
                _curBase.NarrativeEnded -= HandleNarrativeEnd;
            }
            
            GameEvents.Narrative.OnStartNarrative -= OnStartNarrative;
            _animationController.NarrativeEnded -= HandleNarrativeEnd;
        }

        void OnStartNarrative(INarrativeData obj) {
            switch (obj.type) {
                case NarrativeTypes.Comics: {
                    HandleComics((obj as ComicsData));
                    break;
                }
                case NarrativeTypes.Other: {
                    HandleOtherNarrative((obj as OtherNarrativeData));
                    break;
                }
                case NarrativeTypes.UIAnimation: {
                    HandleUIAnimation(obj as UIAnimationData);
                    break;
                }
            }
        }

        void HandleComics(ComicsData data) {
            _comicsRoot.DestroyChildren();

            _comicsRoot.SetActive(true);
            
            _curBase = Instantiate(data.comicsPrefab, _comicsRoot.transform).GetComponent<NarrativeBase>();

            _curBase.NarrativeEnded += HandleNarrativeEnd;

            // comics.NarrativeEnded += 
        }

        void HandleOtherNarrative(OtherNarrativeData data) {
            switch (data.otherType) {
                case OtherNarrativeTypes.HideNarrativeViews: {
                    _comicsRoot.SetActive(false);
                    break;
                }
            }

            HandleNarrativeEnd();
        }

        void HandleUIAnimation(UIAnimationData data) {
            _animationController.PlayAnimation(data.animationTriggerName);
        }

        void HandleNarrativeEnd() {

            if (_curBase) {
                _curBase.NarrativeEnded -= HandleNarrativeEnd;
                _curBase = null;
            }
            
            GameEvents.Narrative.OnEndNarrative?.Invoke();
        }


    }
}