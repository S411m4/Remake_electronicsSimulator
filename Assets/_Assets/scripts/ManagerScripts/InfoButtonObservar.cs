using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfoButtonObservar : MonoBehaviour
{

    [SerializeField] DrawerInfo drawerInfo;

    private void Start()
    {

        PlayerInput.Instance.OnInteractWithObjects += PlayerInput_OnShowInfo;
    }

    private void PlayerInput_OnShowInfo(object sender, System.EventArgs e)
    {


        if (drawerInfo.IsInRegion()) { drawerInfo.ButtonPressBehaviour(); }
    }
}
