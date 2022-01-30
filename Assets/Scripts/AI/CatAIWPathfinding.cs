using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatAIWPathfinding : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public GameObject target;

    // Start is called before the first frame update
    void Awake()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = Random.Range(1.4f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
        RotateToNextWaypoint();
    }

    void RotateToNextWaypoint()
    {
        if (navMeshAgent.path == null || navMeshAgent.path.corners.Length < 2) return;
        var diff = navMeshAgent.path.corners[1] - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    void MoveTowardsPlayer()
    {
        if (target != null && navMeshAgent.enabled == true)
            navMeshAgent.destination = target.transform.position;
        else
            enabled = false;
    }

    private void OnDrawGizmos()
    {
        if (navMeshAgent != null && navMeshAgent.path != null && navMeshAgent.path.corners.Length > 1)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(navMeshAgent.path.corners[1], 0.5f);
            //Debug.Log("Works");
        }
    }
}
