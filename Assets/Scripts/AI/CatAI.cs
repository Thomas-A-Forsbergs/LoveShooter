using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : MonoBehaviour
{
    CatAIWPathfinding pathfinding;
    static GameObject player;
    public DamageType weakness;
    public GameObject Hearts;
    public static int activeCats = 0;

    private void Awake()
    {
        pathfinding = GetComponent<CatAIWPathfinding>();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        pathfinding.target = player;
        activeCats++;
    }

    private void OnDestroy()
    {
        activeCats--;
    }

    public void Hit(DamageType damageType)
    {
        if(damageType == weakness)
        OnHit();
        
    }

    public void OnHit()
    {
        enabled = false;
        GetComponent<SimplestAnimatorPossible>().enabled = false;
        foreach (var collider in GetComponents<Collider2D>())
        {
            Instantiate (Hearts, transform.position, Quaternion.identity);
            collider.enabled = false;
        } 
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        var almostSimplestAnimator = GetComponent<AlmostSimplestAnimator>();
        almostSimplestAnimator.enabled = true;
        almostSimplestAnimator.onCompleted.AddListener(delegate { Destroy(gameObject); });
    }

}
