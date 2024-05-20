using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireBehaviour : MonoBehaviour
{
    [SerializeField] LineRenderer wireLine;
    private Transform currentTerminalTransform = null;
    private Transform connectedTerminalTransform = null;

    private Transform parentSocket; //name of socket to which this wire is attached

    private GameObject[] wireTerminals;

 

    private const string WIRE_TAG = "wire";

    private void Awake()
    {
        currentTerminalTransform = this.transform;
        wireLine.positionCount = 2;

        //line disappear when other terminal not connected
        wireLine.SetPosition(0, currentTerminalTransform.position);
        wireLine.SetPosition(1, currentTerminalTransform.position);

    }

    private void Start()
    {
        if (connectedTerminalTransform == null)
        { InstantiateWire.Instance.OnConnectWireInstantiated += InstantiateWire_OnConnectWireInstantiated; }
    }

   
    private void InstantiateWire_OnConnectWireInstantiated(object sender, System.EventArgs e)
    {
        

        wireTerminals = GameObject.FindGameObjectsWithTag(WIRE_TAG);

        foreach (GameObject wireTerminal in wireTerminals)
        {
            //check terminal is not connected to anything and to not connect it to itself
            if (wireTerminal.GetComponent<WireBehaviour>().connectedTerminalTransform == null && wireTerminal.GetComponent<WireBehaviour>().currentTerminalTransform != this.gameObject.transform)
            {
                this.connectedTerminalTransform = wireTerminal.transform;
                wireTerminal.GetComponent<WireBehaviour>().connectedTerminalTransform = this.transform;

            }
        }
        InstantiateWire.Instance.OnConnectWireInstantiated -= InstantiateWire_OnConnectWireInstantiated;
        

    }

    private void OnDisable()
    {
        InstantiateWire.Instance.OnConnectWireInstantiated -= InstantiateWire_OnConnectWireInstantiated;
    }

    private void Update()
    {
        //to keep following terminal position when changes
        if (connectedTerminalTransform != null)
        {
            wireLine.SetPosition(0, currentTerminalTransform.position);
            wireLine.SetPosition(1, connectedTerminalTransform.position);
        }
    }

    public GameObject GetConnectedTerminalGameObject()
    {
        if (connectedTerminalTransform != null)
            return connectedTerminalTransform.gameObject;
        else
            return null;
    }

    public void SetParentSocket(Transform parentSocket)
    {
        this.parentSocket = parentSocket;
    }

    public Transform GetParentSocket()
    {
        return parentSocket;
    }

    public void UnSetConnectedTermianl() { connectedTerminalTransform = null; }
    public void UnSetCurrentTermianl() { currentTerminalTransform = null; }
}
