using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuStylizedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image Arrow;
    public Material DefaultTextMaterial, OutlinedTextMaterial;
    public string Description;
    public AudioClip HoverAudio, ClickAudio;
    [ReadOnly]
    public bool IsHovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Arrow.DOColor(Color.white, 0.15f);
        Arrow.rectTransform.DOLocalMoveX(-100f, 0.15f).From();
        GetComponentInChildren<TextMeshProUGUI>().fontMaterial = OutlinedTextMaterial;
        IsHovering = true;
        AudioPoolManager.Instance.PlaySound(HoverAudio, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Arrow.DOColor(Color.white * new Vector4(1, 1, 1, 0), 0.15f);
        GetComponentInChildren<TextMeshProUGUI>().fontMaterial = DefaultTextMaterial;
        IsHovering = false;
    }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(()=> AudioPoolManager.Instance.PlaySound(ClickAudio, 1f));
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(()=> AudioPoolManager.Instance.PlaySound(ClickAudio, 1f));
    }
}
