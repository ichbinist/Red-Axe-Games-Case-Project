using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; }

    private SellerController sellerController;
    public SellerController SellerController { get { return (sellerController == null) ? sellerController = GetComponentInParent<SellerController>() : sellerController; } }

    public void Interact(CharacterSettings characterSettings)
    {
        if (characterSettings.CanControl)
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