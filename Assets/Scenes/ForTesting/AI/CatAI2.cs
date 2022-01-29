using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI2 : MonoBehaviour
{
    public float direction = 0;
    Rigidbody2D rigidbody;
    static GameObject player;
    public float rotationSpeed = 90;

    //If there is no obstacle, head straight towards the player
    //If there is an obstacle, turn away from it (prioritizing turning towards the player)





    // Start is called before the first frame update
    void Awake()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(AngleToDirection(direction));
        if (IsCloserToRightThanLeft(player.transform.position))
        {
            direction -= rotationSpeed * Time.deltaTime;
        }
        else
            direction += rotationSpeed * Time.deltaTime;
        LowAngleToPlayer();
    }

    bool LowAngleToPlayer()
    {
        Debug.Log(Vector2.Angle(transform.position, player.transform.position));
        return false;
    }

    bool IsCloserToRightThanLeft(Vector3 target)
    {
        return Vector3.Distance(target, transform.position + LocalAngleToDirection(90)) > Vector3.Distance(target, transform.position + LocalAngleToDirection(-90));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + LocalAngleToDirection(-90), 1);
        Gizmos.DrawWireSphere(transform.position + LocalAngleToDirection(90), 1);
    }

    Vector3 LocalAngleToDirection(float direction)
    {
        return AngleToDirection(direction + this.direction);
    }
    Vector3 AngleToDirection(float direction)
    {
        return Quaternion.AngleAxis(direction, Vector3.forward) * Vector3.right;
    }
}
