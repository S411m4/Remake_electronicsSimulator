using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DebuggingCanvasUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI polarityTerminalText;
    [SerializeField] private TextMeshProUGUI wireTerminal1Text;
    [SerializeField] private TextMeshProUGUI wireTerminal2Text;
    [SerializeField] private TextMeshProUGUI socketText;
    [SerializeField] private TextMeshProUGUI connectedComponentText;
    [SerializeField] private TextMeshProUGUI brokeAtText;

    [SerializeField] private Image SimulationModeCheck;
    [SerializeField] private Image connectedCheck;
    [SerializeField] private Image polarityCheck;

    
    [SerializeField] private Material NotConnectedColor;
    [SerializeField] private Material ConnectedColor;

    public static DebuggingCanvasUI Instance { get; private set; }

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

        polarityTerminalText.text = "null";
        wireTerminal1Text.text = "null";
        wireTerminal2Text.text = "null";
        socketText.text = "null";
        brokeAtText.text = "brokeAt: ";
        connectedComponentText.text = "";
    }

    public void CheckSimulationMode()
    {

       SimulationModeCheck.material = ConnectedColor;

    }

    public void CheckUncheckConnected(bool check)
    {

        if(check)
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
    public void SetPolarityTerminalText(string text)
    {
        polarityTerminalText.text = text;
    }

    public void SetWireTerminal1Text(string text)
    {
        wireTerminal1Text.text = text;
    }

    public void SetBrokeAtText(string text)
    {
        brokeAtText.text = text;
    }

    public void SetwireTerminal2Text(string text)
    {
        wireTerminal2Text.text = text;
    }

    public void SetSocketText(string text)
    {
        socketText.text = text;
    }

    public void SetConnectedObjectsText(string text)
    {
        string oldText = connectedComponentText.text;
        connectedComponentText.text = oldText  + text + " , ";
    }



}
