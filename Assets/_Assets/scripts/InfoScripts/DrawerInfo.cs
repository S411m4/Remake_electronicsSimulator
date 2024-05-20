using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerInfo : MonoBehaviour
{
    [SerializeField] GameObject infoCanvas;

    [SerializeField] Transform originalDrawerPosition;
    [SerializeField] GameObject drawer;

    private bool inDrawerRegion = false;

    XRGrabInteractable xrGrabInteractable;

    private void Awake()
    {
        xrGrabInteractable = GetComponent<XRGrabInteractable>();
    }
    private void Start()
    {
        Hide();
        xrGrabInteractable.hoverEntered.AddListener((OnHoverEnter)=> { Show();inDrawerRegion = true; }) ;
        xrGrabInteractable.hoverExited.AddListener((HoverExitEventArgs) => { Hide();inDrawerRegion = false; });
    }


    public void ButtonPressBehaviour() { drawer.transform.position = originalDrawerPosition.position; } //fix drawer position
    private void Show() { infoCanvas.SetActive(true); }
    private void Hide() { infoCanvas.SetActive(false); }


    public bool IsInRegion() { return inDrawerRegion; }
}
