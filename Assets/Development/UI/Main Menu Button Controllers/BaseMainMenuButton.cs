using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMainMenuButton : MonoBehaviour
{
    private Button button;
    public Button MainMenuButton { get { return (button == null) ? button = GetComponent<Button>() : button; } }

    public void OnEnable()
    {
        MainMenuButton.onClick.AddListener(OnClick);
    }

    public void OnDisable()
    {
        MainMenuButton.onClick.RemoveListener(OnClick);
    }

    public abstract void OnClick();
}
