using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyObjectByHands : MonoBehaviour
{

    [SerializeField] XRDirectInteractor leftHand;

    private void Start()
    {
        PlayerInput.Instance.OnDestroyComponent += PlayerInput_OnDestroyComponent;
    }

    private void PlayerInput_OnDestroyComponent(object sender, System.EventArgs e)
    {

        IXRInteractable grabbedInteractable = leftHand.GetOldestInteractableSelected();

        if (grabbedInteractable == null) return;
        GameObject grabbedObject = grabbedInteractable.transform.gameObject;

        DestroyComponent.DestroyComponentObjectWithItsTerminals(grabbedObject);

        SoundEffectsManager.Instance.OnDestroyObjectByHand(leftHand.transform.position);
    }
}

