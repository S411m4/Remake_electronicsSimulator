using UnityEngine;
using UnityEngine.UI;

public class ComponentsCanvasUI_navigationHome : MonoBehaviour
{

    [SerializeField] Button homeButton;
    [SerializeField] Button ledsButton;
    [SerializeField] Button resistorButton;
    [SerializeField] Button batteryButton;

    [SerializeField] GameObject homeCanvas;
    [SerializeField] GameObject ledCanvas;
    [SerializeField] GameObject resistorCanvas;
    [SerializeField] GameObject batteryCanvas;

    [SerializeField] GameObject homeText;
    [SerializeField] GameObject ledText;
    [SerializeField] GameObject resistorText;
    [SerializeField] GameObject batteryText;



    private void Awake()
    {
        DisableAll();
        homeCanvas.SetActive(true);
        homeText.SetActive(true);
    }

    private void Start()
    {
        homeButton.onClick.AddListener(() => { NavigateToHomeCanvas(); SoundEffectsManager.Instance.NormalClick(); });
        ledsButton.onClick.AddListener(() => { NavigateToLedCanvas(); SoundEffectsManager.Instance.NormalClick(); });
        resistorButton.onClick.AddListener(() =>{ NavigateToResistorCanvas(); SoundEffectsManager.Instance.NormalClick(); });
        batteryButton.onClick.AddListener(()=> { NavigateToBatteryCanvas(); SoundEffectsManager.Instance.NormalClick(); });
    }

    private void NavigateToHomeCanvas()
    {
        DisableAll();
        homeCanvas.SetActive(true);
        homeText.SetActive(true);
    }

    private void NavigateToLedCanvas()
    {
        DisableAll();
        ledCanvas.SetActive(true);
        ledText.SetActive(true);
    }

    private void NavigateToResistorCanvas()
    {
        DisableAll();
        resistorCanvas.SetActive(true);
        resistorText.SetActive(true);
    }

    private void NavigateToBatteryCanvas()
    {
        DisableAll();
        batteryCanvas.SetActive(true);
        batteryText.SetActive(true);
    }

    private void DisableAll()
    {
        homeCanvas.SetActive(false);
        ledCanvas.SetActive(false);
        resistorCanvas.SetActive(false);
        batteryCanvas.SetActive(false);

        homeText.SetActive(false);
        ledText.SetActive(false);
        resistorText.SetActive(false);
        batteryText.SetActive(false);
    }

}
