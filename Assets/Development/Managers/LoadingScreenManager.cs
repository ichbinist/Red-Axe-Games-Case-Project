using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : Singleton<LoadingScreenManager>
{
    public CanvasGroup SplashScreenPanelCanvasGroup;
    public Action OnLoadingScreenStarted;
    public Action OnLoadingScreenCompleted;

    public CanvasGroup LoadingPanel, PressSpacePanel;
    public Image LevelChangingEffect;
    public Image SplashScreenEffect;
    private bool IsLoadingCompleted;

    public void LoadingScreenActivate()
    {
        OnLoadingScreenStarted?.Invoke();
        SplashScreenPanelCanvasGroup.alpha = 1f;
        SplashScreenPanelCanvasGroup.blocksRaycasts = true;
        GuardAndGarden.Utilities.DelayedCall(() => { IsLoadingCompleted = true; LoadingPanel.alpha = 0; }, 2.5f);
    }

    private void Update()
    {
        if (IsLoadingCompleted)
        {
            PressSpacePanel.alpha += 0.5f * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsLoadingCompleted = false;
                LevelChangingEffect.gameObject.SetActive(true);
                PressSpacePanel.alpha = 0f;
                LevelChangingEffect.DOColor(Vector4.zero, 0.6f).OnComplete(() =>
                {
                    LevelChangingEffect.gameObject.SetActive(false);
                    OnLoadingScreenCompleted?.Invoke();
                    SplashScreenPanelCanvasGroup.alpha = 0f;
                    SplashScreenPanelCanvasGroup.blocksRaycasts = false;
                    SceneManagement.Instance.LoadLevel("UI");
                }).SetEase(Ease.Linear).SetDelay(0.2f).OnStart(() => SplashScreenEffect.gameObject.SetActive(false));
            }
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
