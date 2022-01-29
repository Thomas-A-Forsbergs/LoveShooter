using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : MonoBehaviour
{
    CatAIWPathfinding pathfinding;
    static GameObject player;
    public DamageType weakness;

    private void Awake()
    {
        pathfinding = GetComponent<CatAIWPathfinding>();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        pathfinding.target = player;
    }

    public void Hit(DamageType damageType)
    {
        if(damageType == weakness)
        OnHit();
    }

    public void OnHit()
    {
        Destroy(gameObject);
    }
}
