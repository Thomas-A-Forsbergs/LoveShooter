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

    public void OnHit(DamageType damageType)
    {
        if(damageType == weakness)
        OnRepelled();
    }

    public void OnRepelled()
    {
        Destroy(gameObject);
    }

    public enum DamageType
    {
        Love,
        Water
    }
}
