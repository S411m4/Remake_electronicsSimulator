using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCSourceBehaviour : ComponentBehaviourBase, IGetCurrentVoltRatingsAffectingCurrentBehaviour, IGetComponentPrefab, IGetComponentInfo
{
    [SerializeField] DCSourceSO sourceSO; //volt and current passing through connected circuit
    [SerializeField] CurrentBehaviour current;

    float IGetCurrentVoltRatingsAffectingCurrentBehaviour.GetComponentVoltRating()
    {
        return sourceSO.volt_V;
    }

    float IGetCurrentVoltRatingsAffectingCurrentBehaviour.GetComponentCurrentRating()
    {
        return sourceSO.current_mA;
    }

    public override float AffectingVolt(float volt, float current)
    {
        return volt + sourceSO.volt_V;
    }


    GameObject IGetComponentPrefab.GetComponentPrefab()
    { return sourceSO.prefab; }

    string IGetComponentPrefab.GetComponentName()
    { return sourceSO.name; }


    float IGetComponentInfo.GetVoltInfo() { return this.GetComponent<IGetCurrentVoltRatingsAffectingCurrentBehaviour>().GetComponentVoltRating(); }

    float IGetComponentInfo.GetCurrentInfo() { return current.GetComponent<CurrentBehaviour>().GetCurrent(); }
    string IGetComponentInfo.GetState()
    {
        if (current.GetComponent<CheckConnections>().IsConnectedCircuit() && LabGameManager.Instance.IsSimulationModeActive()) { return "connected"; }
        else { return "not connected"; }

    }
}
