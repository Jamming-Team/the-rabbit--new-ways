using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rabbit {
    public class SceneLoaderM {
        public bool inProgress { get; private set; }

        public IEnumerator LoadScene(string sceneNameToLoad) {
            inProgress = true;
            
            var scenes = new List<string>();
            var sceneCount = SceneManager.sceneCount;

            for (var i = sceneCount - 1; i >= 0; i--) {
                var sceneAt = SceneManager.GetSceneAt(i);
                if (!sceneAt.isLoaded) continue;

                var sceneName = sceneAt.name;
                if (sceneName == GC.Scenes.CORE) continue;
                scenes.Add(sceneName);
            }

            foreach (var scene in scenes) yield return SceneManager.UnloadSceneAsync(scene);

            // Optional: UnloadUnusedAssets - unloads all unused assets from memory
            System.GC.Collect();
            yield return Resources.UnloadUnusedAssets();

            yield return SceneManager.LoadSceneAsync(sceneNameToLoad, LoadSceneMode.Additive);

            var activeScene = SceneManager.GetSceneByName(sceneNameToLoad);

            if (activeScene.IsValid()) SceneManager.SetActiveScene(activeScene);
            
            inProgress = false;
        }
    }
}