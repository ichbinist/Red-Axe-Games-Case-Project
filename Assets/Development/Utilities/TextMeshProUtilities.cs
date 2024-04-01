using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class TextMeshProUtilities
{
    public static List<TextMeshProData> TextMeshProDataList = new List<TextMeshProData>();

    public static void SetRedesignedText(this TextMeshProUGUI textMeshProUGUI, string targetText)
    {
        TextMeshProData textMeshProData = new TextMeshProData(textMeshProUGUI.text, textMeshProUGUI);
        TextMeshProDataList.Add(textMeshProData);

        string localString = textMeshProUGUI.text;
        localString = localString.Replace("xxxxx", targetText);
        textMeshProUGUI.SetText(localString);
    }

    public static void SetRedesignedText(this TextMeshProUGUI textMeshProUGUI, float targetText)
    {
        TextMeshProData textMeshProData = new TextMeshProData(textMeshProUGUI.text, textMeshProUGUI);
        TextMeshProDataList.Add(textMeshProData);

        string localString = textMeshProUGUI.text;
        localString = localString.Replace("xxxxx", targetText.ToString("F1"));
        textMeshProUGUI.SetText(localString);
    }

    public static void SetRedesignedText(this TextMeshProUGUI textMeshProUGUI, int targetText)
    {
        TextMeshProData textMeshProData = new TextMeshProData(textMeshProUGUI.text, textMeshProUGUI);
        TextMeshProDataList.Add(textMeshProData);

        string localString = textMeshProUGUI.text;
        localString = localString.Replace("xxxxx", targetText.ToString());
        textMeshProUGUI.SetText(localString);
    }

    public static void RestoreText(this TextMeshProUGUI textMeshProUGUI)
    {
        textMeshProUGUI.SetText(TextMeshProDataList.Find(x => x._textMeshProUGUI == textMeshProUGUI).oldText);
    }
}

public class TextMeshProData
{
    public string oldText;
    public TextMeshProUGUI _textMeshProUGUI;

    public TextMeshProData(string _oldText, TextMeshProUGUI textMeshProUGUI)
    {
        oldText = _oldText;
        _textMeshProUGUI = textMeshProUGUI;
    }
}