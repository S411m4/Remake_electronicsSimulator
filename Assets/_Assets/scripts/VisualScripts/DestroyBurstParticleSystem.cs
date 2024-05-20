using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBurstParticleSystem : MonoBehaviour
{
    [SerializeField] ParticleSystem componentDestroyBrust;

    private void Start()
    {
        var em = componentDestroyBrust.emission;
        em.enabled = true;
        em.rateOverTime = 0;

        em.SetBurst(1,new ParticleSystem.Burst(1.0f, 50));
    }
}
