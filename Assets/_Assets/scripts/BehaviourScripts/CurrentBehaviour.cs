using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentBehaviour : MonoBehaviour
{
    [SerializeField] CheckConnections checkConnections;

    private float volt = 0f;
    private float current = 0f;


    private void Start()
    {
        checkConnections.OncircuitConnected += CheckConnections_OncircuitConnected;
    }

    private void OnDisable()
    {
       
        checkConnections.OncircuitConnected -= CheckConnections_OncircuitConnected;
    }
    private void CheckConnections_OncircuitConnected(object sender, CheckConnections.OnCircuitConnectedEventArgs e)
    {
        volt = 0f;
        current = 0f;
        //get hightest current drawn from component to be the total current drawn from battery
        float highestCurrentDrawn = 0f;
        foreach (GameObject component in e.connectedComponentsInCircuit)
        {
            if (highestCurrentDrawn < Mathf.Abs(component.GetComponent<IGetCurrentVoltRatingsAffectingCurrentBehaviour>().GetComponentCurrentRating()))
            {

                highestCurrentDrawn = component.GetComponent<IGetCurrentVoltRatingsAffectingCurrentBehaviour>().GetComponentCurrentRating(); 
             }
        }

        current = highestCurrentDrawn; //negative value of current (drawn)

        foreach (GameObject component in e.connectedComponentsInCircuit)
        {
            component.GetComponent<ComponentBehaviourBase>().Behaviour(volt, current);
            current = component.GetComponent<ComponentBehaviourBase>().AffectingCurrent(current, volt);
            volt = component.GetComponent<ComponentBehaviourBase>().AffectingVolt(volt, current);
            
        }

    }

    public float GetCurrent() { return current; }

}
