using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rabbit
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] SceneLoaderV _view;
        readonly SceneLoaderM _model = new();
        
        bool _is_loading = false;

        public void TryLoadScene(string sceneName, bool withoutAnims = false)
        {
            if (_is_loading)
                return;
            
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        
        private IEnumerator LoadSceneAsync(string sceneName, bool withoutAnims = false)
        {
            _is_loading = true;
            
            if (withoutAnims)
            {
                _view.SetAnim(SceneLoaderV.LoadingAnims.In);
                yield return new WaitUntil(() => _view.animEndedTrigger);
            }

            StartCoroutine(_model.LoadScene(sceneName));
            
            // yield return modelCor;
            yield return new WaitUntil(() => _model.loadingEndedTrigger);

            if (withoutAnims)
            {
                _view.SetAnim(SceneLoaderV.LoadingAnims.Out);
            }
            
            _is_loading = false;
        }
        
    }
}
