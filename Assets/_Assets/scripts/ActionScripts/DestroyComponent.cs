using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public static class  DestroyComponent
{
    private enum PolarityTerminal { cathode, anode }

    public static void DestroyComponentObjectWithItsTerminals(GameObject grabbedComponentObject)
    {
        DestroyConnectedToPolarityTerminal(PolarityTerminal.anode, grabbedComponentObject);
        DestroyConnectedToPolarityTerminal(PolarityTerminal.cathode, grabbedComponentObject);


        //destroy object its
        grabbedComponentObject.GetComponent<XRBaseInteractable>().colliders.Clear();
        MonoBehaviour.Destroy(grabbedComponentObject);
    }


    private static void DestroyConnectedToPolarityTerminal(PolarityTerminal polarityTerminal, GameObject grabbedComponentObject)
    {
        IXRSelectInteractable wireTerminalConnectedToPolarityTerminalInteractable = null;
        GameObject wireTerminalConnectedToPolarityTerminalObject = null;
        GameObject connectedTo = null;

        switch (polarityTerminal)
        {
            case PolarityTerminal.anode:
                wireTerminalConnectedToPolarityTerminalInteractable = grabbedComponentObject.GetComponent<IGetComponentPolarity>().GetAnode().GetComponent<XRSocketInteractor>().GetOldestInteractableSelected();
                break;

            case PolarityTerminal.cathode:
                wireTerminalConnectedToPolarityTerminalInteractable = grabbedComponentObject.GetComponent<IGetComponentPolarity>().GetCathode().GetComponent<XRSocketInteractor>().GetOldestInteractableSelected();
                break;

        }

        if (wireTerminalConnectedToPolarityTerminalInteractable == null) return;
        wireTerminalConnectedToPolarityTerminalObject = wireTerminalConnectedToPolarityTerminalInteractable.transform.gameObject;

        connectedTo = wireTerminalConnectedToPolarityTerminalObject.GetComponent<WireBehaviour>().GetConnectedTerminalGameObject();

        wireTerminalConnectedToPolarityTerminalObject.GetComponent<WireBehaviour>().UnSetConnectedTermianl();

        wireTerminalConnectedToPolarityTerminalObject.GetComponent<XRBaseInteractable>().colliders.Clear();
        MonoBehaviour.Destroy(wireTerminalConnectedToPolarityTerminalObject);

        if (connectedTo == null) return;

        wireTerminalConnectedToPolarityTerminalObject.GetComponent<WireBehaviour>().UnSetCurrentTermianl();

        connectedTo.GetComponent<XRBaseInteractable>().colliders.Clear();
        MonoBehaviour.Destroy(connectedTo);
    }
}
