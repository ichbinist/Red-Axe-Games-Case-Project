using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

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
    [FoldoutGroup("Movement Settings")]
    public bool CanControl = true;

    [FoldoutGroup("Interaction Settings")]
    public bool CanInterract = true;
    [FoldoutGroup("Interaction Settings")]
    public float InteractionRange = 7.5f;

    [FoldoutGroup("Input Settings")]
    public KeyCode InteractionKey = KeyCode.E;

    [FoldoutGroup("References")]
    public RCC_Camera RCC_Camera;
    public void LockInteraction()
    {
        CanInterract = false;
    }

    public void UnlockInteraction()
    {
        CanInterract = true;
    }

    public void LockControls()
    {
        CanControl = false;
    }

    public void UnlockControls()
    {
        CanControl = true;
    }

    public void SetParent(Transform transform)
    {
        this.transform.parent = transform;
    }

    public void ClearParent()
    {
        this.transform.parent = null;
    }
    
    public void UseCar(RCC_CarControllerV3 carController)
    {
        LockInteraction();
        LockControls();
        SetParent(transform);
        RCC_Camera.SetTarget(carController);
        RCC_Camera.actualCamera.enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        transform.rotation = Quaternion.identity;
        carController.SetEngine(true);
        carController.SetCanControl(true);
    }

    public void LeaveCar(RCC_CarControllerV3 carController)
    {
        UnlockControls();
        UnlockInteraction();
        ClearParent();
        transform.rotation = Quaternion.identity;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        RCC_Camera.actualCamera.enabled = false;
        carController.SetEngine(false);
        carController.SetCanControl(false);
    }
}
