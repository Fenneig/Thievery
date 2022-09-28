using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    
    public enum Scene
    {
        Level_00,
        MainMenu,
        LoadingScene,
        Credits
    }
    public static class SceneLoader
    {
        private static Action _onLoaderCallback;
        private static AsyncOperation _asyncOperation;

        public static void LoadScene(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }

        public static IEnumerator LoadSceneAsync(Scene scene)
        {
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
            _onLoaderCallback += () => SceneManager.LoadScene(scene.ToString());
            yield return null;

            _asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

            while (!_asyncOperation.isDone)
            {
                yield return null;
            }
        }

        public static float GetLoadingProgress()
        {
            return _asyncOperation?.progress ?? 0f;
        }

        public static void LoaderCallback()
        {
            _onLoaderCallback?.Invoke();
            _onLoaderCallback = null;
        }
    }
}