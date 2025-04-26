using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Rabbit {
    public class DialogueHeadController : MonoBehaviour {
        [SerializeField] Animator _animator;

        [SerializeField] Image _headImage;
        
        Sequence _sequence;
        Vector3 _defaultScale = Vector3.one;
        Vector3 _increasedScale = new Vector3(1.5f, 1.5f, 1f);
        bool _shouldIncrease;


        public void ChangeSize(bool shouldIncrease) {
            if (_sequence.isAlive)
                _sequence.Complete();
            
            if (_shouldIncrease == shouldIncrease)
                return;
            
            _shouldIncrease = shouldIncrease;
            
            if (shouldIncrease)
                _sequence = Sequence.Create()
                    .Chain(Tween.Custom(_defaultScale, _increasedScale, 1f, newVal => _headImage.transform.localScale = newVal));
            else
                _sequence = Sequence.Create()
                    .Chain(Tween.Custom(_increasedScale, _defaultScale, 1f, newVal => _headImage.transform.localScale = newVal));
        }
        

        public static class HeadAnimsNames {
            public static int increase = Animator.StringToHash("Increase");
            public static int decrease = Animator.StringToHash("Decrease");
        }
    }
}