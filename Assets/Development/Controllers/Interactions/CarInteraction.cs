using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInteraction : MonoBehaviour, IInteractable
{
    private RCC_CarControllerV3 carController;
    public RCC_CarControllerV3 CarController { get { return (carController == null) ? carController = GetComponentInParent<RCC_CarControllerV3>(): carController; } }

    public bool IsInteractable { get; set; }
    public string InteractableText { get => "<color=#00FF00>E</color> to Ride"; }

    [ReadOnly]
    public bool IsPlayerRidingCar;

    private CharacterSettings CharacterSettings;

    private void OnEnable()
    {
        //Ge�ici, Test i�in
        IsInteractable = false;
    }

    public void Interact(CharacterSettings characterSettings)
    {
        if (IsInteractable)
        {
            CharacterSettings = characterSettings;
            characterSettings.UseCar(CarController);
            StartCoroutine(CarInteractionBuffer(true));
        }
    }

    private void Update()
    {
        LeaveCar();
    }

    public void LeaveCar()
    {
        if (IsPlayerRidingCar)
        {
            if (CharacterSettings)
            {
                if (Input.GetKeyDown(CharacterSettings.InteractionKey))
                {
                    CharacterSettings.LeaveCar(CarController);
                    StartCoroutine(CarInteractionBuffer(false));
                }
            }
        }
    }

    private IEnumerator CarInteractionBuffer(bool state)
    {
        yield return new WaitForSeconds(0.1f);
        IsPlayerRidingCar = state;
    }
}
