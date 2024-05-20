using UnityEngine;
using System;

public class InstantiateWire : MonoBehaviour
{
    [SerializeField] private GameObject wirePrefab;
    [SerializeField] private Transform InstantiatePosition;

    private const string WIRE_TAG = "wire";
    public event EventHandler OnConnectWireInstantiated;




    public static InstantiateWire Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        PlayerInput.Instance.OnIndexTriggerWire += PlayerInput_OnIndexTriggerWire;

    }

    private void PlayerInput_OnIndexTriggerWire(object sender, System.EventArgs e)
    {
        if (LabGameManager.Instance.IsSimulationModeActive()) return;

        //dont instantiate if there is already a wire instantiated and not yet connected
        foreach(Transform child in this.transform)
        {
            if(child.tag == WIRE_TAG) return;
        }

        
        GameObject instantiatedWireTerminal = Instantiate(wirePrefab, this.transform);
        instantiatedWireTerminal.transform.position = InstantiatePosition.position;

        OnConnectWireInstantiated?.Invoke(this, EventArgs.Empty);

    }
}
