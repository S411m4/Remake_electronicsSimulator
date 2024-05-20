using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class CheckConnections : MonoBehaviour
{
    private bool connectedCircuit = true;
   

    [SerializeField] GameObject battery;

    const string NO_POLARITY_TAG = "noPolarity";

    public event EventHandler<OnCircuitConnectedEventArgs> OncircuitConnected;
    public class OnCircuitConnectedEventArgs : EventArgs { public List<GameObject> connectedComponentsInCircuit; }
   
    

    private void Start()
    {
        LabGameManager.Instance.OnSimulationCheckConnection += LabGameManager_OnSimulationCheckConnection;
    }

    private void OnDisable()
    {
        LabGameManager.Instance.OnSimulationCheckConnection -= LabGameManager_OnSimulationCheckConnection;
    }
    private void LabGameManager_OnSimulationCheckConnection(object sender, System.EventArgs e)
    {
        List<GameObject> connectedComponentsInCircuit = new List<GameObject>();

        //starting from anode of the battery
        connectedCircuit = true;


        //int i = 0;

        GameObject connectedPolarityTerminalOfComponent = battery.GetComponent<IGetComponentPolarity>().GetAnode();  //component polarity terminal

        while (connectedPolarityTerminalOfComponent != battery.GetComponent<IGetComponentPolarity>().GetCathode())
        {
            //Debug.Log("loop " + i);
            //Debug.Log("connected polarity terminal: "+ connectedPolarityTerminalOfComponent.name);
            
            string previousTag = connectedPolarityTerminalOfComponent.tag;  //to compare polarity

            //Debug.Log("previous tag: " + previousTag);
            connectedComponentsInCircuit.Add(connectedPolarityTerminalOfComponent.GetComponent<SocketBehaviour>().GetSocketParent().gameObject); //add parent component to list
            
            //DebuggingCanvasUI.Instance.SetConnectedObjectsText(connectedPolarityTerminalOfComponent.transform.parent.name);
            ConnectionUI.Instance.SetConnectedObjectsText(connectedPolarityTerminalOfComponent.transform.parent.name);



            //DebuggingCanvasUI.Instance.SetPolarityTerminalText(connectedPolarityTerminalOfComponent.name);

            XRSocketInteractor polarityTerminalSocket = connectedPolarityTerminalOfComponent.GetComponent<XRSocketInteractor>();  //socket of polarity terminal
            IXRSelectInteractable wireConneectedToPolarityTerminal = polarityTerminalSocket.GetOldestInteractableSelected(); //wire terminal connected to polarity terminal


            if (wireConneectedToPolarityTerminal == null) //Debug.Log("broke at wireconnectedtopolarityTerminal");
            { connectedCircuit = false;  ConnectionUI.Instance.SetBrokeAtText(connectedPolarityTerminalOfComponent.transform.parent.name); break; }  //DebuggingCanvasUI.Instance.SetBrokeAtText("wireTerminal1");

            //DebuggingCanvasUI.Instance.SetWireTerminal1Text(wireConneectedToPolarityTerminal.transform.gameObject.name);

            GameObject otherWireTerminal = wireConneectedToPolarityTerminal.transform.gameObject.GetComponent<WireBehaviour>().GetConnectedTerminalGameObject(); //other wireTerminal of wire

            if (otherWireTerminal == null) // Debug.Log("broke at otherwireterminal"); 
            { connectedCircuit = false;ConnectionUI.Instance.SetBrokeAtText(connectedPolarityTerminalOfComponent.transform.parent.name); break; }//DebuggingCanvasUI.Instance.SetBrokeAtText("wireTerminal2");
            
            //DebuggingCanvasUI.Instance.SetwireTerminal2Text(otherWireTerminal.ToString());

            Transform parentSocketAttachedToOtherTerminal = otherWireTerminal.GetComponent<WireBehaviour>().GetParentSocket(); //get parent socket of other terminal


            if (parentSocketAttachedToOtherTerminal == null)//Debug.Log("brokeat parentsocketattachedtootherterminal");
            { connectedCircuit = false;  ConnectionUI.Instance.SetBrokeAtText(connectedPolarityTerminalOfComponent.transform.parent.name); break; }//DebuggingCanvasUI.Instance.SetBrokeAtText("polarityTerminal2");

            //Debug.Log("parent socket attachedtootherterminal(tag): " +  parentSocketAttachedToOtherTerminal.gameObject.tag);
            //checking polarity in series
            if (!(previousTag == parentSocketAttachedToOtherTerminal.tag || parentSocketAttachedToOtherTerminal.tag == NO_POLARITY_TAG || previousTag == NO_POLARITY_TAG))
            { connectedCircuit = false; ConnectionUI.Instance.UncheckPolarity();
                ConnectionUI.Instance.SetBrokeAtText(parentSocketAttachedToOtherTerminal.GetComponent<SocketBehaviour>().GetSocketParent().name + "  (Polarity)");
                 }// DebuggingCanvasUI.Instance.UncheckPolarity();


            Transform parentComponent = parentSocketAttachedToOtherTerminal.GetComponent<SocketBehaviour>().GetSocketParent(); //parent component (led, resistor, battery, ...)
            //DebuggingCanvasUI.Instance.SetSocketText(parentComponent.name);
            //Debug.Log("other parent component: "+parentComponent);

            if (parentComponent.GetComponent<ComponentBehaviourBase>().IsComponentBurnt()) //component is burnt
            { connectedCircuit = false; ConnectionUI.Instance.SetBrokeAtText(parentComponent.name + " (Burnt)"); break; }; //DebuggingCanvasUI.Instance.SetBrokeAtText("Brunt " + parentComponent.name);

            connectedPolarityTerminalOfComponent = parentComponent.GetComponent<IGetComponentPolarity>().GetCathode(); //get cathode (other terminal) because we checked for anode

            //i++;

        }


        //DebuggingCanvasUI.Instance.CheckUncheckConnected(connectedCircuit);
        ConnectionUI.Instance.CheckUncheckConnected(connectedCircuit);

        if (!connectedCircuit) { connectedComponentsInCircuit.Clear(); }

        OncircuitConnected?.Invoke(this, new OnCircuitConnectedEventArgs { connectedComponentsInCircuit = connectedComponentsInCircuit });

        //Debug.Log("components in circuit");
        //foreach(GameObject component in connectedComponentsInCircuit)
        //{ Debug.Log(component.name); }

    }

    public bool IsConnectedCircuit() { return connectedCircuit; }


    
}

