using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuExitButton : BaseMainMenuButton
{
    public override void OnClick()
    {
        Application.Quit();
    }
}
