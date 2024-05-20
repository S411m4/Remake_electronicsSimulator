using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class testScript : MonoBehaviour
{
    [SerializeField] float testno;
    private void Update()
    {
        Debug.Log(testno.ConvertToUnit('m'));
        Debug.Log(testno.ConvertToUnit('u'));
        Debug.Log(testno.ConvertToUnit('n'));
        Debug.Log(testno.ConvertToUnit('K'));
        Debug.Log(testno.ConvertToUnit('M'));
        Debug.Log(testno.ConvertToUnit('G'));
        Debug.Log(testno.ConvertToUnit('o'));
    }
}


