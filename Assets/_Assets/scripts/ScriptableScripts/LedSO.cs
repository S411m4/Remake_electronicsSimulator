using UnityEngine;


[CreateAssetMenu()]
public class LedSO : ScriptableObject
{
    public string name;
    public GameObject prefab;
    public Sprite schematicSymbol;
    public float voltDrop_Volts;
    public float currentDrawn_mA;
    public Material ledColor;
    public Material emissionMaterial;

}
