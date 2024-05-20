using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInput : MonoBehaviour
{
    //custom event triggered by script, fire them
    public event EventHandler OnIndexTriggerWire;
    public event EventHandler OnSimulationModeActivateDeactivate;
    public event EventHandler OnDestroyComponent;
    public event EventHandler OnInteractWithObjects;
    public event EventHandler<OnShowComponentInfoEventArgs> OnShowComponentInfo;
    public class OnShowComponentInfoEventArgs : EventArgs {public GameObject currentHeldComponentRightHand; }

    //xr plugin events , subscribe to them
    [SerializeField] private InputActionReference OnWireRightHandXRAction;
    [SerializeField] private InputActionReference OnSimulationModeRightHandPrimaryButtonAction;
    [SerializeField] private InputActionReference OnDestroyComponentPrimaryLeftButtonAction;
    [SerializeField] private InputActionReference OnShowInfoSecondaryLeftButtonAction;
    [SerializeField] private InputActionReference OnShowComponentInfoSecondaryRightButtonAction;


    public static PlayerInput Instance { get; private set; }

    [SerializeField] XRDirectInteractor rightHand;
 
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        OnWireRightHandXRAction.action.performed += XRRightHand_OnWire;
        OnSimulationModeRightHandPrimaryButtonAction.action.performed += RightHandPrimaryButton_OnSimulationModeActivateDeactivate;
        OnDestroyComponentPrimaryLeftButtonAction.action.performed += PrimaryLeftButton_OnDestroyComponent;
        OnShowInfoSecondaryLeftButtonAction.action.performed += SecondaryLeftButton_OnInteractWithObjects;
        OnShowComponentInfoSecondaryRightButtonAction.action.performed += SecondaryRightButton_OnShowComponentInfo;


    }

    private void SecondaryRightButton_OnShowComponentInfo(InputAction.CallbackContext obj)
    {
        IXRInteractable currentHeldInteractable = rightHand.GetOldestInteractableSelected();

        if (currentHeldInteractable == null) return;

        OnShowComponentInfo?.Invoke(this,new OnShowComponentInfoEventArgs { currentHeldComponentRightHand = currentHeldInteractable.transform.gameObject });
    }

    private void SecondaryLeftButton_OnInteractWithObjects(InputAction.CallbackContext obj)
    {
        if (OnShowInfoSecondaryLeftButtonAction.action.IsPressed())
        { OnInteractWithObjects?.Invoke(this, EventArgs.Empty); }
    }

    private void PrimaryLeftButton_OnDestroyComponent(InputAction.CallbackContext obj)
    {
        if (OnDestroyComponentPrimaryLeftButtonAction.action.IsPressed())
        { OnDestroyComponent?.Invoke(this, EventArgs.Empty); }
    }

    private void RightHandPrimaryButton_OnSimulationModeActivateDeactivate(InputAction.CallbackContext obj)
    {
        if (OnSimulationModeRightHandPrimaryButtonAction.action.IsPressed())
        { OnSimulationModeActivateDeactivate?.Invoke(this, EventArgs.Empty); }
    }


    private void XRRightHand_OnWire(InputAction.CallbackContext obj)
    {
        OnIndexTriggerWire?.Invoke(this, EventArgs.Empty);
    }
}
