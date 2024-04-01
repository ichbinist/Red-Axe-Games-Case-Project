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
        SellerManager.Instance.OnSellerDataUpdate += OnSellerInitialize;
        AcceptButton.onClick.AddListener(AcceptButtonAction);
        HaggleButton.onClick.AddListener(HaggleButtonAction);
        RefuseButton.onClick.AddListener(RefuseButtonAction);
        SellerManager.Instance.OnSellerInteractionFinished += OnSellerFinished;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (SellerManager.Instance)
        {
            SellerManager.Instance.OnSellerInteractionStarted -= OnSellerInitialize;
            SellerManager.Instance.OnSellerDataUpdate -= OnSellerInitialize;
            SellerManager.Instance.OnSellerInteractionFinished -= OnSellerFinished;
        }

        AcceptButton.onClick.RemoveListener(AcceptButtonAction);
        HaggleButton.onClick.RemoveListener(HaggleButtonAction);
        RefuseButton.onClick.RemoveListener(RefuseButtonAction);
    }

    private void Update()
    {
        if (IsPanelActive && sellerInteractionData != null)
        {
            if (!WealthManager.Instance.DoesHaveEnoughWealth(sellerInteractionData.ModifiedValue))
            {
                HaggleButton.interactable = false;
                AcceptButton.interactable = false;
                return;
            }

            string inputText = BuyerProposalInputArea.text;

            float value;

            if (float.TryParse(inputText, out value))
            {
                if(value != sellerInteractionData.ModifiedValue)
                {
                    AcceptButton.interactable = false;
                    HaggleButton.interactable = true;
                }
                else
                {
                    AcceptButton.interactable = true;
                    HaggleButton.interactable = false;
                }
            }
            else
            {
                HaggleButton.interactable = false;
                AcceptButton.interactable = false;
            }
        }
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

    protected void OnSellerFinished(SellerInteractionData sellerInteractionData)
    {
        ClosePanel();
        ClearTexts();
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
        string inputText = BuyerProposalInputArea.text;

        int value;

        if (int.TryParse(inputText, out value))
        {
            Debug.Log(value);
            SellerManager.Instance.OnHaggle.Invoke((float)value);
        }
    }

    protected void RefuseButtonAction()
    {
        ClosePanel();
        ClearTexts();
        SellerManager.Instance.OnRefuse?.Invoke();
    }
}
