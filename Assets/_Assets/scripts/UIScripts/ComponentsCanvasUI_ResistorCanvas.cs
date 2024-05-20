using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class ComponentsCanvasUI_ResistorCanvas : MonoBehaviour
{
    [SerializeField] KeypadUI keypad;
    [SerializeField] TextMeshProUGUI feedbackText;

    [SerializeField] Image band1Image;
    [SerializeField] Image band2Image;
    [SerializeField] Image multiplierImage;

    [SerializeField] AllResistorBandColorsSO resistorBandColorsSO;

    [SerializeField] Material defaultMaterial;

    [SerializeField] GameObject resistorPrefab;
    [SerializeField] Transform instantiatePosition;

    public event EventHandler OnResistorValuesSet;

    public static ComponentsCanvasUI_ResistorCanvas Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        keypad.OnEnterPressed += Keypad_OnEnterPressed;
        keypad.OnValueChange += Keypad_OnValueChange;
    }

    private void Keypad_OnValueChange(object sender, System.EventArgs e)
    {
        feedbackText.text = "";
    }

    private void Keypad_OnEnterPressed(object sender, KeypadUI.OnEnterPressedEventArgs e)
    { 
        if(e.noText.text == "" || e.unitText.text == "" || !correctValueEnteredCheck(e.noText.text)) { feedbackText.text = "please enter resistor value and unit"; return;}

        double NearestRealresistanceValueOhms = GetNearestRealValueFromText(Convert.ToDouble(e.noText.text), e.unitText.text[0]);


        int[] resValueArray = SplitValuesintoDigits(NearestRealresistanceValueOhms);


        GameObject instantiatedRes = Instantiate(resistorPrefab.gameObject , instantiatePosition.position, Quaternion.identity);

        instantiatedRes.GetComponent<ResistorValues>().ResistorValueOhm = NearestRealresistanceValueOhms;

        Material band1Material = ConvertNoToMaterial(resValueArray[0]);
        instantiatedRes.GetComponent<ResistorValues>().band1Color = band1Material;
        band1Image.color = band1Material.color;

        Material band2Material = ConvertNoToMaterial(resValueArray[1]);
        instantiatedRes.GetComponent<ResistorValues>().band2Color = band2Material;
        band2Image.color = band2Material.color;

        Material multiplierBandMaterial = ConvertNoToMaterial(resValueArray[2]);
        instantiatedRes.GetComponent<ResistorValues>().multiplierBandColor = multiplierBandMaterial;
        multiplierImage.color = multiplierBandMaterial.color;


        OnResistorValuesSet?.Invoke(this, EventArgs.Empty);

    }

    private double GetNearestRealValueFromText(double noTextValue, char symbol)
    {
        
        int[] realValuesArrayGreaterThanTen = {10,11,12,13,15,16,18,20,22,24,27,30,33,36,39,43,47,51,56,62,68,75,82,91};
        float[] realValuesArraySmallerThanTen = {1.0f,1.1f,1.2f,1.3f,1.5f,1.6f,1.8f,2.0f,2.2f,2.4f,2.7f,3.0f,3.3f,3.6f,3.9f,4.3f,4.7f,5.1f,5.6f,6.2f,6.8f,7.5f,8.2f,9.1f };
        const int LargestRealValueGreaterThanTen = 91;
        const float LargestRealValueSmallerThanTen = 9.1f;

        if (noTextValue < 1)
        {
            foreach (float no in realValuesArraySmallerThanTen)
            {
                if ((float)noTextValue * 10 - no <= 0.01f) //didn't compare directly because of float precision
                {
                    feedbackText.text = "nearest real value: " + no / 10 + " " + symbol;
                    return no / 10 * 1f.ConvertToUnit(symbol);
                }

            }
            feedbackText.text = "nearest real value: " + LargestRealValueSmallerThanTen + " " + symbol;
            return LargestRealValueSmallerThanTen / 10 * 1f.ConvertToUnit(symbol);
        }
        else if (noTextValue < 10)
        {
            foreach (float no in realValuesArraySmallerThanTen)
            {
                if ((float)noTextValue - no <= 0.01f)
                {
                    feedbackText.text = "nearest real value: " + no + " " + symbol;
                    return no * 1f.ConvertToUnit(symbol);
                }

            }
            feedbackText.text = "nearest real value: " + LargestRealValueSmallerThanTen + " " + symbol;
            return LargestRealValueSmallerThanTen * 1f.ConvertToUnit(symbol);
        }
        else if (noTextValue < 100)
        {
            foreach (int no in realValuesArrayGreaterThanTen)
            {
                if ((int)noTextValue <= no)
                {
                    feedbackText.text = "nearest real value: " + no + " " + symbol;
                    return no * 1f.ConvertToUnit(symbol);
                }
            }
            feedbackText.text = "nearest real value: " + LargestRealValueGreaterThanTen + " " + symbol;
            return LargestRealValueGreaterThanTen * 1f.ConvertToUnit(symbol);
        }
        else // greater than 100
        {
            foreach (int no in realValuesArrayGreaterThanTen)
            {
                if ((int)noTextValue/10 <= no)
                {
                    feedbackText.text = "nearest real value: " + no*10 + " " + symbol;
                    return no*10 * 1f.ConvertToUnit(symbol);
                }
            }
            feedbackText.text = "nearest real value: " + LargestRealValueGreaterThanTen*10 + " " + symbol;
            return LargestRealValueGreaterThanTen*10 * 1f.ConvertToUnit(symbol);
        }
    }

    private bool correctValueEnteredCheck(string noText)
    {
        int occurenceOfPointChar = 0;
        foreach(char letter in noText)
        {
            if(letter == '.') { occurenceOfPointChar++; }
            if(occurenceOfPointChar > 1) { return false; }
        }
        return true;
    }

    private int[] SplitValuesintoDigits(double resValue)
    {
        int[] result = {0,0,0};

        //Multiplier color
        if(resValue < 100) //0-99 ohm   
        { result[2] = 0;resValue = resValue / 1;}
        else if(resValue < 1000) //100 - 999 ohm    110
        { result[2] = 1; resValue = resValue / 10; }
        else if(resValue < 10000) //1 - 9.99 kohm   1100
        { result[2] = 2; resValue = resValue / 100; }
        else if(resValue < 100000) //10 - 99.9 kohm  11000
        { result[2] = 3; resValue = resValue / 1000;}
        else if(resValue < 1000000) //100 - 999.99 kohm  110000
        { result[2] = 4; resValue = resValue / 10000; }
        else if(resValue < 10000000) //1 - 9.99 Mohm
        {result[2] = 5;resValue = resValue / 100000; }
        else if(resValue< 100000000)
        {result[2] = 6;resValue = resValue / 1000000; } //10 - 99.99 Mohm
        else
        { result[2] = 7;resValue =resValue/ 10000000; }

        result[0] = (int)(resValue / 10);//tens
        result[1] = (int) (((resValue / 10) - result[0]) * 10); //units = (no/10 - tens) *10

        return result;
    }


    private Material ConvertNoToMaterial(int no)
    {
        Material bandMaterial = defaultMaterial;
        switch(no)
        {
            case 0:
                bandMaterial = resistorBandColorsSO.black;
                break;
            case 1:
                bandMaterial = resistorBandColorsSO.brown;
                break;
            case 2:
                bandMaterial = resistorBandColorsSO.red;
                break;
            case 3:
                bandMaterial = resistorBandColorsSO.orange;
                break;
            case 4:
                bandMaterial = resistorBandColorsSO.yellow;
                break;
            case 5:
                bandMaterial = resistorBandColorsSO.green;
                break;
            case 6:
                bandMaterial = resistorBandColorsSO.blue;
                break;
            case 7:
                bandMaterial = resistorBandColorsSO.violet;
                break;
            case 8:
                bandMaterial = resistorBandColorsSO.grey; 
                break;
            case 9:
                bandMaterial = resistorBandColorsSO.white;
                break;
        }

        return bandMaterial;
    }

}
