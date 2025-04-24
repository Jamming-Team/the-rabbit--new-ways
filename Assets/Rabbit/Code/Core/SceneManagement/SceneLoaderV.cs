using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rabbit
{
    public class SceneLoaderV : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        public bool animEndedTrigger {  get; private set; }

        
        public void SetAnim(LoadingAnims anim)
        {
        }

        public void SetAnimEnded()
        {
            animEndedTrigger = true;
        }
        
        
        public enum LoadingAnims
        {
            In,
            Out
        }
    }
}