using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResistorBehaviour : ComponentBehaviourBase, IGetCurrentVoltRatingsAffectingCurrentBehaviour,IGetComponentPrefab
{ 
    [SerializeField] ResistorSO resistorSO;



    [SerializeField] ResistorValues resistorValues;

    public ResistorSO GetResistorSO()
    {
        return resistorSO;
    }

    public override float AffectingVolt(float volt, float current)
    {
        return volt + (float)(current.ConvertToUnit('m') * resistorValues.ResistorValueOhm);
    }

    GameObject IGetComponentPrefab.GetComponentPrefab()
    { return resistorSO.prefab; }

    string IGetComponentPrefab.GetComponentName()
    { return resistorSO.name; }

    

}