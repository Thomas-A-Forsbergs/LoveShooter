using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    public float timeToDestroy = 5;
    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
