using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rabbit {
    public class DialogueController : NarrativeBase {

        [SerializeField] TMP_Text _nameNext;
        [SerializeField] TMP_Text _dialogueText;
        
        [SerializeField] Button _continueButton;
        
        [SerializeField] DialogueHeadController _dialogueHeadLeft;
        [SerializeField] DialogueHeadController _dialogueHeadRight;
        
        [SerializeField] Animator _animator;

        DialogueDataSO _data;
        int _partIndex;

        void Awake() {
            _continueButton.onClick.AddListener(Continue);
        }

        void OnDestroy() {
            _continueButton.onClick.RemoveListener(Continue);
        }

        public void StartDialogue(DialogueDataSO data) {
            _data = data;
            _partIndex = -1;
            _animator.SetTrigger("In");
            Continue();
        }

        void Continue() {
            _partIndex++;

            if (_partIndex >= _data.parts.Count) {
                _animator.SetTrigger("Out");
                NarrativeEnded?.Invoke();
                return;
            }
            
            _dialogueText.text = _data.parts[_partIndex].text;

            switch (_data.parts[_partIndex].type) {
                case SpeakerType.Player: {
                    _nameNext.text = "You";
                    
                    _dialogueHeadLeft.ChangeSize(true);
                    _dialogueHeadRight.ChangeSize(false);
                    break;
                }
                
                case SpeakerType.Shadow: {
                    _nameNext.text = "Shadow";
                    
                    _dialogueHeadLeft.ChangeSize(false);
                    _dialogueHeadRight.ChangeSize(true);
                    break;
                }
            }
            
        }

    }
}