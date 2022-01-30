using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticleEffectsDestruction : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void OnDestroy()
    {
        Instantiate(particleSystem, transform.position, transform.rotation);
        //particleSystem.Play();
    }
}
