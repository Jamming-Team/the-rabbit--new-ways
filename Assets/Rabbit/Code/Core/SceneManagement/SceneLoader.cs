using System.Collections;
using UnityEngine;

namespace Rabbit {
    public class SceneLoader : MonoBehaviour {
        [SerializeField] SceneLoaderV _view;
        readonly SceneLoaderM _model = new();

        bool _isLoading;

        public void TryLoadScene(string sceneName, bool withAnims = false) {
            if (_isLoading) return;

            StartCoroutine(LoadSceneAsync(sceneName, withAnims));
        }

        IEnumerator LoadSceneAsync(string sceneName, bool withAnims = false) {
            _isLoading = true;

            if (withAnims) {
                _view.SetAnim(SceneLoaderV.LoadingAnims.In);
                yield return new WaitUntil(() => !_view.inProgress);
            }

            StartCoroutine(_model.LoadScene(sceneName));

            // yield return modelCor;
            yield return new WaitUntil(() => !_model.inProgress);

            if (withAnims) _view.SetAnim(SceneLoaderV.LoadingAnims.Out);

            _isLoading = false;
        }
    }
}