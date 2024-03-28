using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSplashScreenController : MonoBehaviour
{
    public Image SplashScreenImage;
    public List<Sprite> LoadingScreenSprites = new List<Sprite>();

    private void OnEnable()
    {
        LoadingScreenManager.Instance.OnLoadingScreenStarted += OnLoadingScreen;
    }

    private void OnDisable()
    {
        if(LoadingScreenManager.Instance != null)
            LoadingScreenManager.Instance.OnLoadingScreenStarted += OnLoadingScreen;
    }

    void OnLoadingScreen()
    {
        SplashScreenImage.sprite = LoadingScreenSprites[Random.Range(0, LoadingScreenSprites.Count)];
    }
}
