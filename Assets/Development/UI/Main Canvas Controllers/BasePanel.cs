using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup { get { return (canvasGroup == null) ? canvasGroup = GetComponent<CanvasGroup>() : canvasGroup; } }

    private bool _isPanelActive;

    public bool IsPanelActive
    {
        get => _isPanelActive;
        private set => _isPanelActive = value;
    }

    public Action OnPanelOpened;
    public Action OnPanelClosed;

    protected virtual void OnEnable()
    {
        OnPanelOpened += PanelOpenedAction;
        OnPanelClosed += PanelClosedAction;
    }

    protected virtual void OnDisable()
    {
        OnPanelOpened -= PanelOpenedAction;
        OnPanelClosed -= PanelClosedAction;
    }

    public virtual void OpenPanel()
    {
        CanvasGroup.alpha = 1f;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = true;
        IsPanelActive = true;
        OnPanelOpened?.Invoke();
    }

    public virtual void ClosePanel()
    {
        CanvasGroup.alpha = 0f;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;
        IsPanelActive = false;
        OnPanelClosed?.Invoke();
    }

    public abstract void PanelOpenedAction();
    public abstract void PanelClosedAction();
}
