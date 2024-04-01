using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public bool IsInteractable { get; set; }
    public string InteractableText { get;}


    public void Interact(CharacterSettings characterSettings);
}
