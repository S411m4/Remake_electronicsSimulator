using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyRespawnObjectFallOnGround : MonoBehaviour
{

    [SerializeField] Transform instantiateComponentTransform;
    [SerializeField] int componentLayerNo;


    private void OnCollisionEnter(Collision collision)
    {
  
        if (collision.gameObject.layer == componentLayerNo)
        {
            SoundEffectsManager.Instance.OnComponentFallOnGround(collision.transform.position);

            GameObject falledComponent = collision.gameObject;
  
            GameObject respawnedComponent = Instantiate(falledComponent.GetComponent<IGetComponentPrefab>().GetComponentPrefab(), instantiateComponentTransform.position, instantiateComponentTransform.rotation);

            RespawnedComponentPreset(falledComponent.GetComponent<IGetComponentPrefab>().GetComponentName(), falledComponent,respawnedComponent) ;

            DestroyComponent.DestroyComponentObjectWithItsTerminals(falledComponent);

  
        }
    }



    private enum ComponentNames {Resistor,Led,DCSource }


    private void RespawnedComponentPreset(string name, GameObject falledComponent, GameObject respawnedComponent)
    {
        Enum.TryParse(name, out ComponentNames componentName);

        switch (componentName)
        {
            case ComponentNames.Resistor:
                ResistorPreset(falledComponent, respawnedComponent);
                break;

            case ComponentNames.Led:
                LedPreset(falledComponent, respawnedComponent);
                break;

            case ComponentNames.DCSource:
                break;
                
        }
    }

    private void LedPreset(GameObject falledComponent, GameObject respawnedComponent)
    {
        LedSO ledSO = falledComponent.GetComponent<LedBehaviour>().GetLedSO();
        respawnedComponent.GetComponent<LedBehaviour>().SetLedSO(ledSO);
    }

    private void ResistorPreset(GameObject falledComponent, GameObject respawnedComponent)
    {
        respawnedComponent.GetComponent<ResistorValues>().ResistorValueOhm = falledComponent.GetComponent<ResistorValues>().ResistorValueOhm;
        respawnedComponent.GetComponent<ResistorValues>().band1Color = falledComponent.GetComponent<ResistorValues>().band1Color;
        respawnedComponent.GetComponent<ResistorValues>().band2Color = falledComponent.GetComponent<ResistorValues>().band2Color;
        respawnedComponent.GetComponent<ResistorValues>().multiplierBandColor = falledComponent.GetComponent<ResistorValues>().multiplierBandColor;
    }
}
