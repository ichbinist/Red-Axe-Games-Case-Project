using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPanel : BasePanel
{
    public TextMeshProUGUI InteractionText;

    protected override void OnEnable()
    {
        base.OnEnable();
        InteractionManager.Instance.OnInteractableTargetChange += SetText;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (InteractionManager.Instance)
        {
            InteractionManager.Instance.OnInteractableTargetChange -= SetText;
        }
    }

    public void SetText(string text)
    {
        InteractionText.SetText(text);
    }

    public override void PanelClosedAction()
    {
        throw new System.NotImplementedException();
    }

    public override void PanelOpenedAction()
    {
        throw new System.NotImplementedException();
    }
}
