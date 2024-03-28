using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuStylizedFloatArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Graphic BackgroundImage;
    public TextMeshProUGUI HeaderText;
    public TextMeshProUGUI VolumeText;
    public TextMeshProUGUI VolumeInfoText;
    public List<Image> Images = new List<Image>();
    public Material DefaultTextMaterial, OutlinedTextMaterial;
    public AudioClip HoverAudio;
    [ReadOnly]
    public bool IsHovering;

    public Action OnPointerEnterAction;

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var image in Images)
        {
            image.DOColor(Color.white, 0.15f);
        }
        BackgroundImage.DOColor(new(BackgroundImage.color.r, BackgroundImage.color.g, BackgroundImage.color.b, 1f), 0.2f);
        VolumeText.DOColor(Color.white, 0.2f);
        VolumeInfoText.DOColor(Color.white, 0.2f);
        GetComponentInChildren<TextMeshProUGUI>().fontMaterial = OutlinedTextMaterial;
        IsHovering = true;
        AudioPoolManager.Instance.PlaySound(HoverAudio, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var image in Images)
        {
            image.DOColor(Color.white * new Vector4(1, 1, 1, 0), 0.15f);
        }
        BackgroundImage.DOColor(new(BackgroundImage.color.r, BackgroundImage.color.g, BackgroundImage.color.b, 0f), 0.2f);
        VolumeText.DOColor(Color.white * new Vector4(1f, 1f, 1f, 0f), 0.2f);
        VolumeInfoText.DOColor(Color.white * new Vector4(1f, 1f, 1f, 0f), 0.2f);

        GetComponentInChildren<TextMeshProUGUI>().fontMaterial = DefaultTextMaterial;
        IsHovering = false;
    }
}
