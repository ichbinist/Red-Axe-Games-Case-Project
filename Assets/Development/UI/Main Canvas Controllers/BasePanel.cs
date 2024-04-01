using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup { get { return (canvasGroup == null) ? canvasGroup = GetComponent<CanvasGroup>() : canvasGroup; } }
}
