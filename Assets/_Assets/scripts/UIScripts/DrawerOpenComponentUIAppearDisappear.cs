using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOpenComponentUIAppearDisappear : MonoBehaviour
{
    [SerializeField] GameObject drawer;
    [SerializeField] GameObject componentCanvas;

    private void Awake()
    {
        Hide();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == drawer)
        { Show(); }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject == drawer)
        { Hide(); }
    }

    private void Show() { componentCanvas.SetActive(true); }
    private void Hide() { componentCanvas.SetActive(false); }
}
