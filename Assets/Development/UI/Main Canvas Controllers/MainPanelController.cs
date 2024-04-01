using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainPanelController : BasePanel
{
    [FoldoutGroup("References")]
    public TextMeshProUGUI WealthText;

    protected override void OnEnable()
    {
        base.OnEnable();
        WealthManager.Instance.OnWealthUpdated += SetWealth;
        GuardAndGarden.Utilities.DelayedCall(() => WealthManager.Instance.IncreaseWealth(2500), 0.1f); //For Testing
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if ( WealthManager.Instance != null )
            WealthManager.Instance.OnWealthUpdated -= SetWealth;
    }

    private void SetWealth()
    {
        WealthText.SetText(WealthManager.Instance.Wealth.ToString());
    }

    public override void PanelClosedAction()
    {

    }

    public override void PanelOpenedAction()
    {

    }
}
