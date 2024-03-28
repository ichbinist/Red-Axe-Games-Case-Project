using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInteractionController : MonoBehaviour
{
    private CharacterSettings characterSettings;
    private CharacterSettings CharacterSettings { get { return (characterSettings == null) ? characterSettings = GetComponent<CharacterSettings>() : characterSettings; } }
    
    [ShowInInspector]
    [ReadOnly]
    private IInteractable interactable;

    private bool isTargetAvailable { get { return interactable != null; } }

    private void Update()
    {
        CheckInteractionTarget();
        Interact();
    }

    public void CheckInteractionTarget()
    {
        if (CharacterSettings.CanInterract)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, CharacterSettings.InteractionRange, LayerMask.GetMask("Interactable")))
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                if(interactable != null)
                {
                    this.interactable = interactable;
                }
            }
            else
            {
                this.interactable = null;
            }
        }
    }

    public void Interact()
    {
        if (CharacterSettings.CanInterract)
        {
            if (isTargetAvailable)
            {
                if (Input.GetKeyDown(CharacterSettings.InteractionKey))
                {
                    interactable.Interact(CharacterSettings);
                }
            }
        }
    }
}