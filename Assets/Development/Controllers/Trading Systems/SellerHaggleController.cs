using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerHaggleController : MonoBehaviour
{
    [FoldoutGroup("Seller Data")]
    [ReadOnly]
    public SellerInteractionData SellerInteractionData = new SellerInteractionData();
    private void OnEnable()
    {
        SellerManager.Instance.OnSellerInteractionStarted += Initialize;
        SellerManager.Instance.OnHaggle += OnHaggle;
        SellerManager.Instance.OnAccept += OnAccept;
        SellerManager.Instance.OnRefuse += OnRefuse;
    }

    private void OnDisable()
    {
        if(SellerManager.Instance != null)
            SellerManager.Instance.OnSellerInteractionStarted -= Initialize;
    }

    private void Initialize(SellerInteractionData sellerInteractionData)
    {
        SellerInteractionData = sellerInteractionData;
    }

    public void OnHaggle(float purposeValue)
    {
        if(purposeValue >= SellerInteractionData.ModifiedValue)
        {
            AcceptDeal(purposeValue);
        }
        else
        {
            if(purposeValue >= SellerInteractionData.ModifiedValue * 0.9f)
            {
                AcceptDeal(purposeValue);
            }
            else if (purposeValue >= SellerInteractionData.ModifiedValue * 0.75f)
            {
                RepurposeDeal(SellerInteractionData.ModifiedValue * 0.9f);
            }
            else
            {
                CloseDeal();
            }
        }
    }

    public void OnAccept(float purposeValue)
    {
        AcceptDeal(purposeValue);
    }

    public void OnRefuse()
    {
        CloseDeal();
    }

    private void CloseDeal()
    {
        //Deal'ý kapat
        SellerInteractionData.Seller.CharacterSettings.UnlockControls();
        SellerInteractionData.Seller.CharacterSettings.UnlockInteraction();
        SellerInteractionData.Seller.CharacterSettings.UnlockPhysics();
        SellerInteractionData.Seller.CharacterSettings.LockCursor();
    }

    private void AcceptDeal(float acceptedValue)
    {
        //Deal'ý kabul et
        
        SellerInteractionData.Seller.CarSettings.ChangeOwnership(Ownership.Player);
        //Parayý Azalt

        SellerManager.Instance.OnSellerInteractionFinished?.Invoke(SellerInteractionData);
        SellerInteractionData.Seller.CharacterSettings.UnlockControls();
        SellerInteractionData.Seller.CharacterSettings.UnlockInteraction();
        SellerInteractionData.Seller.CharacterSettings.UnlockPhysics();
        SellerInteractionData.Seller.CharacterSettings.LockCursor();
    }

    private void RepurposeDeal(float repurposedValue)
    {
        // Deal'ý düzeltip geri gönder
        SellerInteractionData.ModifiedValue = repurposedValue;
        SellerManager.Instance.OnSellerDataUpdate?.Invoke(SellerInteractionData);
    }
}