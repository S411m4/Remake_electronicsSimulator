using UnityEngine;
using System;

public class LabGameManager : MonoBehaviour
{
    public static LabGameManager Instance { get; private set; }
    public event EventHandler OnSimulationCheckConnection;

    private bool simulationModeActive = false;

    private enum State
    { connection, simulation, coding, schematic};

    State state;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerInput.Instance.OnSimulationModeActivateDeactivate += playerInput_OnSimulationModeActivateDeactivate;
    }

    private void playerInput_OnSimulationModeActivateDeactivate(object sender, System.EventArgs e)
    {
        if(simulationModeActive) //exit simulation mode
        {
            //DebuggingCanvasUI.Instance.ResetCanvas();
            ConnectionUI.Instance.ResetCanvas();
            state = State.connection;
            simulationModeActive = false;
        }
        else //enter simulation mode
        {
            //DebuggingCanvasUI.Instance.CheckSimulationMode();
            ConnectionUI.Instance.CheckSimulationMode();
            state = State.simulation;
            simulationModeActive = true;
            OnSimulationCheckConnection?.Invoke(this, EventArgs.Empty);
        }

    }

    public bool IsSimulationModeActive()
    { return simulationModeActive; }
}
