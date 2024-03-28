using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSettings : MonoBehaviour
{
    [FoldoutGroup("Movement Settings")]
    public float WalkingSpeed = 5f;
    [FoldoutGroup("Movement Settings")]
    public float JumpForce = 10f;
    [FoldoutGroup("Movement Settings")]
    public float MouseSensitivity = 2f;
    [FoldoutGroup("Movement Settings")]
    [ReadOnly]
    public bool IsGrounded;

    [FoldoutGroup("Interaction Settings")]
    [ReadOnly]
    public bool CanInterract = true;
    [FoldoutGroup("Interaction Settings")]
    public float InteractionRange = 7.5f;

    [FoldoutGroup("Input Settings")]
    public KeyCode InteractionKey = KeyCode.E;


    public void LockInteraction()
    {
        CanInterract = false;
    }

    public void UnlockInteraction()
    {
        CanInterract = true;
    }
}
