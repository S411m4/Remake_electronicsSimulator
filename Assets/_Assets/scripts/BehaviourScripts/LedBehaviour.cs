using UnityEngine;
using System;

public class LedBehaviour : ComponentBehaviourBase, IGetCurrentVoltRatingsAffectingCurrentBehaviour, IGetComponentPrefab, IGetComponentInfo
{
    private LedSO ledSO;

    enum LedState
    { On, Off, Burnt };

    private LedState ledState;


    public event EventHandler<OnLedTurnedOnEventArgs> OnLedTurnedOn;
    public class OnLedTurnedOnEventArgs : EventArgs
    { public float ledEmission; }

    public event EventHandler OnLedTurnedOff;
    public event EventHandler OnLedBurnt;

    [SerializeField] const float EMISSION_MULTIPLIER = 3;



    private void Awake()
    {
        ledState = LedState.Off;
    }


    public override void Behaviour(float volt, float current)
    {
        const float LED_BURNING_VOLT_MULTIPLIER = 2.5f;

        if (ledState == LedState.Burnt) return; //once led burn it is kept burnt

        if (volt < Mathf.Abs(ledSO.voltDrop_Volts))
        {
            ledState = LedState.Off;
        }
        else if (volt > Mathf.Abs(ledSO.voltDrop_Volts * LED_BURNING_VOLT_MULTIPLIER))
        {
            ledState = LedState.Burnt;
        }
        else
        {
            //led is on
            ledState = LedState.On;
        }

        switch (ledState)
        {
            case LedState.On:
                float ledEmission = ((volt - ledSO.voltDrop_Volts).Map(0, volt, 0, 1) + 0.1f) * EMISSION_MULTIPLIER;
                OnLedTurnedOn?.Invoke(this, new OnLedTurnedOnEventArgs { ledEmission = ledEmission });
                break;

            case LedState.Off: //either not connected or not enough volt
                OnLedTurnedOff?.Invoke(this, EventArgs.Empty);
                break;

            case LedState.Burnt:
                OnLedBurnt?.Invoke(this, EventArgs.Empty);
                componentBurnt = true;
                break;
        }
    }

    public override float AffectingVolt(float volt, float current)
    {
        return volt + ledSO.voltDrop_Volts;
    }


    public void SetLedSO(LedSO ledSO)
    {
        this.ledSO = ledSO;
    }
    public LedSO GetLedSO()
    {
        return ledSO;
    }

    float IGetCurrentVoltRatingsAffectingCurrentBehaviour.GetComponentCurrentRating()
    {
        return ledSO.currentDrawn_mA;
    }

    float IGetCurrentVoltRatingsAffectingCurrentBehaviour.GetComponentVoltRating()
    {
        return ledSO.voltDrop_Volts;
    }

    GameObject IGetComponentPrefab.GetComponentPrefab()
    { return ledSO.prefab; }

    string IGetComponentPrefab.GetComponentName()
    { return ledSO.name; }

    string IGetComponentInfo.GetState() { return ledState.ToString(); }
    float IGetComponentInfo.GetVoltInfo() { return Mathf.Abs(GetComponent<IGetCurrentVoltRatingsAffectingCurrentBehaviour>().GetComponentVoltRating()); }
    float IGetComponentInfo.GetCurrentInfo() { return Mathf.Abs(GetComponent<IGetCurrentVoltRatingsAffectingCurrentBehaviour>().GetComponentCurrentRating()); }
}
