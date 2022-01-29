using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatAIWPathfinding : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    static GameObject player;
    Rigidbody rigidbody;
    Vector3 lastPosition;

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
        MoveToPlayer();
        RotateWithVelocity();
    }

    void RotateWithVelocity()
    {
        if (navMeshAgent.path == null || navMeshAgent.path.corners.Length < 2) return;
        var diff = navMeshAgent.path.corners[1] - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        //Debug.Log(navMeshAgent.nextPosition);
        
        //transform.right = player.transform.position - transform.position;
        //transform.up = new Vector3(player.transform.position.x, player.transform.position.y, 0) - transform.position;
        //transform.rotation = Quaternion.AngleAxis(Vector3.Angle(transform.position, player.transform.position), Vector3.forward);
    }

    void MoveToPlayer()
    {
        navMeshAgent.destination = player.transform.position;
        lastPosition = transform.position;
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
