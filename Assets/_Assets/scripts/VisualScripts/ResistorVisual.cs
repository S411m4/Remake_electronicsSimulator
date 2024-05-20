using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistorVisual : MonoBehaviour
{

    [SerializeField] GameObject no1Band;
    [SerializeField] GameObject no2Band;
    [SerializeField] GameObject multiplierBand;

    [SerializeField] Material defaultMaterial;

    [SerializeField] ResistorValues resistorValues;
 
    private void OnEnable()
    {
        ComponentsCanvasUI_ResistorCanvas.Instance.OnResistorValuesSet += Instance_OnResistorValuesSet;
    }

    private void OnDisable()
    {
        ComponentsCanvasUI_ResistorCanvas.Instance.OnResistorValuesSet -= Instance_OnResistorValuesSet;

    }
    private void Instance_OnResistorValuesSet(object sender, System.EventArgs e)
    {
        if (resistorValues.band1Color == null || resistorValues.band2Color == null || resistorValues.multiplierBandColor == null)
        {
            no1Band.GetComponent<Renderer>().material = defaultMaterial;
            no2Band.GetComponent<Renderer>().material = defaultMaterial;
            multiplierBand.GetComponent<Renderer>().material = defaultMaterial;
        }
        else
        {

            no1Band.GetComponent<Renderer>().material = resistorValues.band1Color;
            no2Band.GetComponent<Renderer>().material = resistorValues.band2Color;
            multiplierBand.GetComponent<Renderer>().material = resistorValues.multiplierBandColor;
        }
    }

    



}
