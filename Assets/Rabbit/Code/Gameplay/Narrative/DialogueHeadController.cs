using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Rabbit {
    public class DialogueHeadController : MonoBehaviour {

        [SerializeField] Image _headImage;
        
        Sequence _sequence;
        Sequence _sequence2;
        Vector3 _defaultScale = Vector3.one;
        Vector3 _increasedScale = new Vector3(1.5f, 1.5f, 1f);
        bool _shouldIncrease;
        Vector3 _rotationVector = new Vector3(0f, 0f, 7);

        public void Awake() {
            // Sequence.Create()
            //     .Chain(Tween.ShakeLocalRotation(_headImage.transform, strength: new Vector3(0, 0, 4), duration: 10000,
            //         frequency: 6));
        }

        void OnDestroy() {
            if (_sequence.isAlive)
                _sequence.Complete();
            
            if (_sequence2.isAlive)
                _sequence2.Complete();
        }


        public void ChangeSize(bool shouldIncrease) {
            if (_sequence.isAlive)
                _sequence.Complete();
            
            if (_shouldIncrease == shouldIncrease)
                return;
            
            _shouldIncrease = shouldIncrease;

            if (shouldIncrease) {
                _sequence = Sequence.Create()
                    .Chain(Tween.Custom(_defaultScale, _increasedScale, 1f,
                        newVal => _headImage.transform.localScale = newVal));
                _sequence2 = Sequence.Create()
                    .Chain(Tween.ShakeLocalRotation(_headImage.transform, strength: new Vector3(0, 0, 4), duration: 10000,
                        frequency: 6));
            }
            else {
                _sequence = Sequence.Create()
                    .Chain(Tween.Custom(_increasedScale, _defaultScale, 1f,
                        newVal => _headImage.transform.localScale = newVal));
                _sequence2.Complete();
            }
        }
    }
}