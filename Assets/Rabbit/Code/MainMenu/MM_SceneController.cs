using System.Collections.Generic;
using UnityEngine;

namespace Rabbit {
    public class MM_SceneController : MonoBehaviour {
        [SerializeField] protected StateMachine _stateMachine;

        protected virtual void Start() {
            _stateMachine.Init(this, true);

            AudioManager.Instance.PlayMusic(MusicBundleType.MainMenu);
        }
    }
}

