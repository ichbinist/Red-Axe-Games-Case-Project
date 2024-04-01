using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPlayButton : BaseMainMenuButton
{
    private bool IsLevelLoading;
    public CanvasGroup MainMenuCanvasGroup;
    public override void OnClick()
    {
        if (!IsLevelLoading)
        {
            IsLevelLoading = true;

            SceneManagement.Instance.LoadLevel("Gameplay");
            MainMenuCanvasGroup.alpha = 0f;
            MainMenuCanvasGroup.interactable = false;
            LoadingScreenManager.Instance.LoadingScreenActivate();
            SceneManagement.Instance.UnloadLevel("Main Menu");
        }
    }
}
