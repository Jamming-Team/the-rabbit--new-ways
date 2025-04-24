using System.Collections;
using UnityEngine;

namespace Rabbit {
    public class SceneLoader : MonoBehaviour {
        [SerializeField] SceneLoaderV _view;
        readonly SceneLoaderM _model = new();

        bool _isLoading;

        public void TryLoadScene(string sceneName, bool withoutAnims = false) {
            if (_isLoading) return;

            StartCoroutine(LoadSceneAsync(sceneName));
        }

        IEnumerator LoadSceneAsync(string sceneName, bool withoutAnims = false) {
            _isLoading = true;

            if (withoutAnims) {
                _view.SetAnim(SceneLoaderV.LoadingAnims.In);
                yield return new WaitUntil(() => _view.animEndedTrigger);
            }

            StartCoroutine(_model.LoadScene(sceneName));

            // yield return modelCor;
            yield return new WaitUntil(() => _model.loadingEndedTrigger);

            if (withoutAnims) _view.SetAnim(SceneLoaderV.LoadingAnims.Out);

            _isLoading = false;
        }
    }
}