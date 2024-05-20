using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentsCanvasUI_ledsCanvas : MonoBehaviour
{
    [SerializeField] AllLedsSO ledColors;
    
    private enum Colors { red,yellow,green,blue,white };

    [SerializeField] Button redLedButton;
    [SerializeField] Button yellowLedButton;
    [SerializeField] Button greenLedButton;
    [SerializeField] Button blueLedButton;
    [SerializeField] Button whiteLedButton;

    [SerializeField] Transform instantiateposition;

    private void Start()
    {
        redLedButton.onClick.AddListener(()=> { InstantiateLedColor(Colors.red); SoundEffectsManager.Instance.OnAnyComponentInstantiated(); }); 
        yellowLedButton.onClick.AddListener(()=> { InstantiateLedColor(Colors.yellow); SoundEffectsManager.Instance.OnAnyComponentInstantiated(); }); 
        greenLedButton.onClick.AddListener(()=> { InstantiateLedColor(Colors.green); SoundEffectsManager.Instance.OnAnyComponentInstantiated(); }); 
        blueLedButton.onClick.AddListener(()=> { InstantiateLedColor(Colors.blue); SoundEffectsManager.Instance.OnAnyComponentInstantiated(); }); 
        whiteLedButton.onClick.AddListener(()=> { InstantiateLedColor(Colors.white); SoundEffectsManager.Instance.OnAnyComponentInstantiated(); }); 
    }
    private void InstantiateLedColor(Colors colors)
    {
        LedSO ledToInstantiateSO;

        switch(colors)
        {
            case Colors.red:
                ledToInstantiateSO =  ledColors.redLed;
                break;

            case Colors.yellow:
                ledToInstantiateSO = ledColors.yellowLed;
                break;

            case Colors.green:
                ledToInstantiateSO = ledColors.greenLed;
                break;
            case Colors.blue:
                ledToInstantiateSO = ledColors.blueLed;
                break;
            case Colors.white:
                ledToInstantiateSO = ledColors.whiteLed;
                break;
            default:
                ledToInstantiateSO = null;
                break;
        }

        if (ledToInstantiateSO != null)
        {
            GameObject instantaitedLed = Instantiate(ledToInstantiateSO.prefab, instantiateposition.position, Quaternion.identity);
            instantaitedLed.GetComponent<LedBehaviour>().SetLedSO(ledToInstantiateSO);
        }

    }

   
}
