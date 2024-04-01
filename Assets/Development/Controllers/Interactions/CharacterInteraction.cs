using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; }

    private SellerController sellerController;
    public SellerController SellerController { get { return (sellerController == null) ? sellerController = GetComponentInParent<SellerController>() : sellerController; } }

    private void OnEnable()
    {
        SellerManager.Instance.OnSellerInteractionFinished += SellerInteractionFinishedAction;
        IsInteractable = true;
    }

    private void OnDisable()
    {
        if(SellerManager.Instance)
            SellerManager.Instance.OnSellerInteractionFinished -= SellerInteractionFinishedAction;
    }

    private void SellerInteractionFinishedAction(SellerInteractionData sellerInteractionData)
    {
        IsInteractable = false;
        SellerController.CarSettings.ChangeOwnership(Ownership.Player);
    }

    public void Interact(CharacterSettings characterSettings)
    {
        if (characterSettings.CanControl && IsInteractable)
        {
            characterSettings.LockControls();
            characterSettings.LockInteraction();
            characterSettings.LockPhysics();
            characterSettings.UnlockCursor();
            SellerController.CharacterSettings = characterSettings;
            SellerController.SellerTradeInteraction();
        }
    }
}