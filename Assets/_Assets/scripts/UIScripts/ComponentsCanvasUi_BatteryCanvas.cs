using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComponentsCanvasUi_BatteryCanvas : MonoBehaviour
{
    [SerializeField] Button battery9vButton;
    [SerializeField] DCSourceSO battery9vSO;
    [SerializeField] Transform instantiatePosition;

    private void Start()
    {
        battery9vButton.onClick.AddListener(() => { Instantiate(battery9vSO.prefab, instantiatePosition.position, Quaternion.identity); SoundEffectsManager.Instance.OnAnyComponentInstantiated(); });

    }
}
