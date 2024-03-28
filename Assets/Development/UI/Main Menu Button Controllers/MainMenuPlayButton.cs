using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPlayButton : BaseMainMenuButton
{
    private bool IsLevelLoading;
    public override void OnClick()
    {
        if (!IsLevelLoading)
        {
            IsLevelLoading = true;

            SceneManagement.Instance.LoadLevel("Gameplay");
            LoadingScreenManager.Instance.LoadingScreenActivate();
            SceneManagement.Instance.UnloadLevel("Main Menu");
        }
    }
}
