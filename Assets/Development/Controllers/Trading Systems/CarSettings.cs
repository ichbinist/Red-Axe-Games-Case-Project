using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSettings : MonoBehaviour
{
    private RCC_CarControllerV3 carController;
    public RCC_CarControllerV3 CarController { get { return (carController == null) ? carController = GetComponent<RCC_CarControllerV3>() : carController; } }

    private CarInteraction carInteraction;
    public CarInteraction CarInteraction { get { return (carInteraction == null) ? carInteraction = GetComponentInChildren<CarInteraction>() : carInteraction; } }
    [FoldoutGroup("Car Settings")]
    public string CarName;
    [FoldoutGroup("Ownership Settings")]
    [ReadOnly]
    public Ownership Ownership = Ownership.NPC;

    public void ChangeOwnership(Ownership ownership)
    {
        Ownership = ownership;

        if(ownership == Ownership.Player)
        {
            CarInteraction.IsInteractable = true;
        }
        else
        {
            CarInteraction.IsInteractable = false;
        }
    }
}


public enum Ownership
{
    NPC,
    Player
}