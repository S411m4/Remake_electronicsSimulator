using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class SocketBehaviour : MonoBehaviour
{
     XRSocketInteractor socket;
    [SerializeField] Transform parent;

    private const string CONNECTED_WIRE_INTERACTIONLAYER = "connectedWire";

    public static event EventHandler<OnAnyWireSnapEventArgs> OnAnyWireSnap;
    public class OnAnyWireSnapEventArgs : EventArgs { public Vector3 position; }

    private void Awake()
    {
        socket = this.GetComponent<XRSocketInteractor>();
    }

    private void Start()
    { 
        socket.selectEntered.AddListener(SetparentSocketNameOfAttachedInteractable);
        socket.selectExited.AddListener(UnSetParentSocketNameOfAttachedInteractable);
 

    }

    private void OnDisable()
    {
      
        socket.selectEntered.RemoveListener(SetparentSocketNameOfAttachedInteractable);
        socket.selectExited.RemoveListener(UnSetParentSocketNameOfAttachedInteractable);

    }
    private void SetparentSocketNameOfAttachedInteractable(SelectEnterEventArgs e)
    {
        
        IXRSelectInteractable attachedInteractableToSocket = socket.GetOldestInteractableSelected();

        if (attachedInteractableToSocket == null) return;

        GameObject attachedObjectToSocket = attachedInteractableToSocket.transform.gameObject;
        attachedObjectToSocket.GetComponent<WireBehaviour>().SetParentSocket(this.gameObject.transform);


        //once wire connected cannot be removed to avoid strange floating of wire
        attachedObjectToSocket.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask(CONNECTED_WIRE_INTERACTIONLAYER);

        OnAnyWireSnap?.Invoke(this, new OnAnyWireSnapEventArgs { position = this.transform.position }) ;
    
    }

    //won't really be of much use because the wire will never leave the socket 
    private void UnSetParentSocketNameOfAttachedInteractable(SelectExitEventArgs e)
    {
      
        IXRSelectInteractable attachedInteractableToSocket = socket.GetOldestInteractableSelected();

        if (attachedInteractableToSocket == null) return;

        GameObject attachedObjectToSocket = attachedInteractableToSocket.transform.gameObject;
        attachedObjectToSocket.GetComponent<WireBehaviour>().SetParentSocket(null);
        
    }

    public Transform GetSocketParent()
    {
        return parent;
    }

   
}
