using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBehaviourBase : MonoBehaviour, IGetComponentPolarity
{
    [SerializeField] GameObject anodeOrSocket1;
    [SerializeField] GameObject cathodeOrSocket2;

    protected bool componentBurnt = false;

    GameObject IGetComponentPolarity.GetAnode()
    {
        return anodeOrSocket1;
    }

    GameObject IGetComponentPolarity.GetCathode()
    {
        return cathodeOrSocket2;
    }

    public virtual float AffectingCurrent(float current, float volt)
    {
        return current;
    }

    public virtual float AffectingVolt(float volt, float current)
    {
        return volt;
    }

    public virtual void Behaviour(float volt, float current)
    {

    }

    public virtual bool IsComponentBurnt()
    { return componentBurnt; }
}
