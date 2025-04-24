using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rabbit {
    public class SceneLoaderV : MonoBehaviour {
        [SerializeField] Animator _animator;
        [SerializeField] GameObject _camera;
        public bool inProgress { get; private set; }

        public void SetAnim(LoadingAnims anim) {
            inProgress = true;
            switch (anim) {
                case LoadingAnims.In: {
                    _animator.SetTrigger(AnimParams.IN);
                    break;
                }
                case LoadingAnims.Out: {
                    _animator.SetTrigger(AnimParams.OUT);
                    break;
                }
            }
        }

        public void SetAnimEnded(LoadingAnims type) {
            _camera.SetActive(type == LoadingAnims.In);
            inProgress = false;
        }

        public enum LoadingAnims {
            In,
            Out
        }

        static class AnimParams {
            public static int IN = Animator.StringToHash("In");
            public static int OUT = Animator.StringToHash("Out");
        }
    }
}