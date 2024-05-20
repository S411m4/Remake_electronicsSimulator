using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComponentInfoUI : MonoBehaviour
{
    [SerializeField] GameObject componentCanvas;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI voltText;
    [SerializeField] TextMeshProUGUI currentText;
    [SerializeField] TextMeshProUGUI stateText;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        PlayerInput.Instance.OnShowComponentInfo += PlayerInput_OnShowComponentInfo;
    }

    private void OnDisable()
    {
        PlayerInput.Instance.OnShowComponentInfo -= PlayerInput_OnShowComponentInfo;
    }

    private void PlayerInput_OnShowComponentInfo(object sender, PlayerInput.OnShowComponentInfoEventArgs e)
    {
        if (e.currentHeldComponentRightHand == this.gameObject)
        {
            Show();
            UpdateComponentInfo();
        }
        else
        { Hide(); }
    }

    private void UpdateComponentInfo()
    {
        nameText.text = GetComponent<IGetComponentPrefab>().GetComponentName();
        voltText.text = GetComponent<IGetComponentInfo>().GetVoltInfo().ToString("##.##") + "V";
        currentText.text = GetComponent<IGetComponentInfo>().GetCurrentInfo().ToString() + "mA";
        stateText.text = GetComponent<IGetComponentInfo>().GetState();
    }

    private void Show() { componentCanvas.SetActive(true); }
    private void Hide() { componentCanvas.SetActive(false); }
}
