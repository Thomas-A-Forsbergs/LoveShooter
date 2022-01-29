using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatAIWPathfinding : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    static GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = player.transform.position;
    }
}
