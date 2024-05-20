using System;
using UnityEngine;

public class ComponentContainer: MonoBehaviour
{
    //ToThinkAbout: 1. multipleContainers for multipleObjects or user choose object to get from one common container
    //that's why you made it using getter and switch case for all component

    [SerializeField] LedSO ledSO1; //for testing
    [SerializeField] LedSO ledSO2; //for testing
    [SerializeField] ResistorSO resistorSO1; //for testing[SerializeField] LedSO ledSO1; //for testing
    [SerializeField] ResistorSO resistorSO2; //for testing

    //private void Update()
    //{ //testing
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        InstantiateSO(resistorSO);
    //        InstantiateSO(ledSO);
    //    }
    //}

    private void Start()
    {
        InstantiateSO(resistorSO1);
        InstantiateSO(resistorSO2);
        InstantiateSO(ledSO1);
        InstantiateSO(ledSO2);
    }

    private void InstantiateSO(ResistorSO resistorSO)
    {
        GameObject instantiatedObject = Instantiate(resistorSO.prefab, transform.position, transform.rotation);
        
    }

    private void InstantiateSO(LedSO ledSO)
    {
        GameObject instantiatedObject = Instantiate(ledSO.prefab, transform.position, transform.rotation);
    }




}
