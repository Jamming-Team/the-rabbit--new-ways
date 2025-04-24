using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rabbit
{
    public class CoreBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static async void Init() {
            Debug.Log("Core Bootstrapper Init");
            await SceneManager.LoadSceneAsync(GC.Scenes.CORE, LoadSceneMode.Additive);
        }
    }
}