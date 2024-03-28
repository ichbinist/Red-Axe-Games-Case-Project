using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuBottomBandController : MonoBehaviour
{
    public TextMeshProUGUI BottomBandText;

    public List<MainMenuStylizedButton> Buttons = new List<MainMenuStylizedButton>();

    public string GetString()
    {
        foreach (var button in Buttons)
        {
            if (button.IsHovering)
            {
                return button.Description;
            }
        }
        return string.Empty;
    }

    private void LateUpdate()
    {
        string text = "<noparse></noparse>"+GetString();
        if(BottomBandText.text != text)
        {
            BottomBandText.SetText(GetString());
        }
    }
}
