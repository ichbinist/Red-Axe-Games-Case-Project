using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaggleFeedbacksPanel : BasePanel
{
    public CanvasGroup SuccessCanvasGroup, FailCanvasGroup;

    protected override void OnEnable()
    {
        base.OnEnable();
        SellerManager.Instance.OnAccept += SuccessCanvasAction;
        SellerManager.Instance.OnRefuse += FailCanvasAction;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (SellerManager.Instance)
        {
            SellerManager.Instance.OnAccept -= SuccessCanvasAction;
            SellerManager.Instance.OnRefuse -= FailCanvasAction;
        }
    }

    #region Open-Close Actions
    public override void PanelClosedAction()
    {

    }

    public override void PanelOpenedAction()
    {

    }
    #endregion

    private void SuccessCanvasAction(float value)
    {
        SuccessCanvasGroup.DOFade(1f, 0.25f).OnComplete(()=> SuccessCanvasGroup.DOFade(0f, 0.25f).SetDelay(0.75f));
    }

    private void FailCanvasAction()
    {
        FailCanvasGroup.DOFade(1f, 0.25f).OnComplete(() => FailCanvasGroup.DOFade(0f, 0.25f).SetDelay(0.75f));
    }
}