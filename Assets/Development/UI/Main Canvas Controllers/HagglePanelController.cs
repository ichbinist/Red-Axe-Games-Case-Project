using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HagglePanelController : BasePanel
{
    [FoldoutGroup("References")]
    [FoldoutGroup("References/TextMeshProUGUI")]
    public TextMeshProUGUI CarNameText;
    [FoldoutGroup("References/TextMeshProUGUI")]
    public TextMeshProUGUI ProducerCompanyText;
    [FoldoutGroup("References/TextMeshProUGUI")]
    public TextMeshProUGUI MaxSpeedText;
    [FoldoutGroup("References/TextMeshProUGUI")]
    public TextMeshProUGUI MaxTorqueText;
    [FoldoutGroup("References/TextMeshProUGUI")]
    public TextMeshProUGUI BrakeTorqueText;
    [FoldoutGroup("References/TextMeshProUGUI")]
    public TextMeshProUGUI CarDamagePercentageText;
    [FoldoutGroup("References/TextMeshProUGUI")]
    public TextMeshProUGUI PaintedPartPercentageText;
    [FoldoutGroup("References/TextMeshProUGUI")]
    public TextMeshProUGUI BaseValueText;
    [FoldoutGroup("References/TextMeshProUGUI")]
    public TextMeshProUGUI MarketValueText;
    [FoldoutGroup("References/TextMeshProUGUI")]
    public TextMeshProUGUI SellerProposalText;
    [FoldoutGroup("References/Sliders")]
    public Image MotorUpgradeLevelSlider;
    [FoldoutGroup("References/Sliders")]
    public Image CarDamagePercentageSlider;
    [FoldoutGroup("References/Sliders")]
    public Image CarPaintedPartPercentageSlider;
    [FoldoutGroup("References/Input")]
    public TMP_InputField BuyerProposalInputArea;
    [FoldoutGroup("References/Buttons")]
    public Button AcceptButton;
    [FoldoutGroup("References/Buttons")]
    public Button HaggleButton;
    [FoldoutGroup("References/Buttons")]
    public Button RefuseButton;

    [ShowInInspector]
    [ReadOnly]
    private SellerInteractionData sellerInteractionData;

    protected override void OnEnable()
    {
        base.OnEnable();

        ClosePanel();
        SellerManager.Instance.OnSellerInteractionStarted += OnSellerInitialize;
        AcceptButton.onClick.AddListener(AcceptButtonAction);
        HaggleButton.onClick.AddListener(HaggleButtonAction);
        RefuseButton.onClick.AddListener(RefuseButtonAction);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (SellerManager.Instance)
        {
            SellerManager.Instance.OnSellerInteractionStarted -= OnSellerInitialize;
        }

        AcceptButton.onClick.RemoveListener(AcceptButtonAction);
        HaggleButton.onClick.RemoveListener(HaggleButtonAction);
        RefuseButton.onClick.RemoveListener(RefuseButtonAction);
    }

    #region PanelArea
    public override void PanelClosedAction()
    {

    }

    public override void PanelOpenedAction()
    {

    }
    #endregion

    protected void OnSellerInitialize(SellerInteractionData sellerInteractionData)
    {
        this.sellerInteractionData = sellerInteractionData;

        //Texts
        CarNameText.SetRedesignedText(sellerInteractionData.CarName);
        ProducerCompanyText.SetRedesignedText(sellerInteractionData.CarProducer);
        MaxSpeedText.SetRedesignedText(sellerInteractionData.MaxSpeed + "KM/H");
        MaxTorqueText.SetRedesignedText(sellerInteractionData.MaxTorque);
        BrakeTorqueText.SetRedesignedText(sellerInteractionData.BrakeTorque);
        CarDamagePercentageText.SetRedesignedText("%" + sellerInteractionData.DamagePercent.ToString());
        PaintedPartPercentageText.SetRedesignedText("%" + sellerInteractionData.PaintedPercent.ToString());
        BaseValueText.SetRedesignedText(sellerInteractionData.BaseValue);
        MarketValueText.SetRedesignedText(sellerInteractionData.ModifiedValue);
        SellerProposalText.SetRedesignedText(sellerInteractionData.ModifiedValue);

        //Sliders
        MotorUpgradeLevelSlider.fillAmount = sellerInteractionData.MotorUpgradeLevel / 5f;
        CarDamagePercentageSlider.fillAmount = sellerInteractionData.DamagePercent / 100f;
        CarPaintedPartPercentageSlider.fillAmount = sellerInteractionData.PaintedPercent / 100f;

        //Input
        BuyerProposalInputArea.text = sellerInteractionData.ModifiedValue.ToString();

        //Buttons
        AcceptButton.interactable = true;
        HaggleButton.interactable = false;
        RefuseButton.interactable = true;

        OpenPanel();
    }

    protected void ClearTexts()
    {
        CarNameText.RestoreText();
        ProducerCompanyText.RestoreText();
        MaxSpeedText.RestoreText();
        MaxTorqueText.RestoreText();
        BrakeTorqueText.RestoreText();
        CarDamagePercentageText.RestoreText();
        PaintedPartPercentageText.RestoreText();
        BaseValueText.RestoreText();
        MarketValueText.RestoreText();
        SellerProposalText.RestoreText();
    }

    protected void AcceptButtonAction()
    {
        ClosePanel();
        ClearTexts();
        SellerManager.Instance.OnAccept?.Invoke(sellerInteractionData.ModifiedValue);
    }

    protected void HaggleButtonAction()
    {

    }

    protected void RefuseButtonAction()
    {
        ClosePanel();
        ClearTexts();
        SellerManager.Instance.OnRefuse?.Invoke();
    }
}
