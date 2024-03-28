using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : Singleton<SceneManagement>
{
    public void LoadLevel(string name)
    {
        StartCoroutine(LoadLevelCo(name));
    }
    public void LoadLevel(string name, Action e)
    {
        StartCoroutine(LoadLevelActionCo(name, e));
    }

    public void UnloadLevel(string name)
    {
        StartCoroutine(UnloadLevelCo(name));
    }

    private IEnumerator LoadLevelCo(string name)
    {
        yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
    }
    private IEnumerator LoadLevelActionCo(string name, Action e)
    {
        yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        e.Invoke();
    }
    private IEnumerator UnloadLevelCo(string name)
    {
        yield return SceneManager.UnloadSceneAsync(name);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
    }
}