using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackButton : BaseMainMenuButton
{
    public Transform MainMenuButtonHolder, SettingsButtonHolder;
    public CanvasGroup MainMenuCanvasGroup;
    public override void OnClick()
    {
        MainMenuButtonHolder.DOLocalMoveX(0f, 0.4f).OnStart(() => {
            MainMenuCanvasGroup.interactable = false;
        }).OnComplete(() => {
            MainMenuCanvasGroup.interactable = true;
        });
        SettingsButtonHolder.DOLocalMoveX(830f, 0.4f);
    }
}
