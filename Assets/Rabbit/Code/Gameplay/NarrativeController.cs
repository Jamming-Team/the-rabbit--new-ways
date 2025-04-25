using System;
using UnityEngine;

namespace Rabbit {
    public class NarrativeController : MonoBehaviour {

        [SerializeField] GameObject _comicsRoot;
        [SerializeField] GameObject _dialogueRoot;


        void Start() {
            GameEvents.Narrative.OnStartNarrative += OnStartNarrative;
        }

        void OnStartNarrative(INarrativeData obj) {
            switch (obj.type) {
                case NarrativeTypes.Comics: {
                    HandleComics((obj as ComicsData));
                    break;
                }
            }
        }

        void HandleComics(ComicsData data) {
            
            _comicsRoot.DestroyChildren();

            var comics = Instantiate(data.comicsPrefab, _comicsRoot.transform).GetComponent<ComicsController>();
            
            // comics.NarrativeEnded += 
        }


        void HandleNarrativeEnd() {
            
        }

    }
}