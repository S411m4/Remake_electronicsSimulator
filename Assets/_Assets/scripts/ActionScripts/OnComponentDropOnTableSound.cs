using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnComponentDropOnTableSound : MonoBehaviour
{

    [SerializeField] private int layerNo = 6;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == layerNo)
        { SoundEffectsManager.Instance.OnComponentDropOnTable(collision.transform.position); }


    }

}
