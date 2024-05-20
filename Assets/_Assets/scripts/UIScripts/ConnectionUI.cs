using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConnectionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI connectedComponentText;
    [SerializeField] private TextMeshProUGUI brokeAtText;

    [SerializeField] private Image SimulationModeCheck;
    [SerializeField] private Image connectedCheck;
    [SerializeField] private Image polarityCheck;


    [SerializeField] private Material NotConnectedColor;
    [SerializeField] private Material ConnectedColor;

    public static ConnectionUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        ResetCanvas();
    }


    public void ResetCanvas()
    {
        connectedCheck.material = ConnectedColor;
        SimulationModeCheck.material = NotConnectedColor;
        polarityCheck.material = ConnectedColor;

      
        brokeAtText.text = "brokeAt: ";
        connectedComponentText.text = "";
    }

    public void CheckSimulationMode()
    {

        SimulationModeCheck.material = ConnectedColor;

    }

    public void CheckUncheckConnected(bool check)
    {

        if (check)
        {
            connectedCheck.material = ConnectedColor;
        }
        else
        {
            connectedCheck.material = NotConnectedColor;
        }

    }


    public void UncheckPolarity()
    {
        polarityCheck.material = NotConnectedColor;
    }

    public void SetBrokeAtText(string text)
    {
        brokeAtText.text = "BrokeAt: "+ text;
    }


    public void SetConnectedObjectsText(string text)
    {
        string oldText = connectedComponentText.text;
        connectedComponentText.text = oldText + text + " , ";
    }



}
