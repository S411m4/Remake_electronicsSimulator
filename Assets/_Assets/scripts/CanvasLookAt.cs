using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CanvasLookAt : MonoBehaviour
{
    [Serializable]
    private enum Mode {LookAt, LookAtInverted, CameraForward, CameraForwardInverted }

    [SerializeField] private Mode mode;

    private void Start()
    {
        switch (mode)
        { case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;

            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;

            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;

            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
