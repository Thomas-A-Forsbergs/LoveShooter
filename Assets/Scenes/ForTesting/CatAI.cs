using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : MonoBehaviour
{
    static GameObject player;
    Rigidbody2D rigidbody;
    [SerializeField] float minSpeed = 1, maxSpeed = 2;

    Vector2 DirectionToPlayer => player.transform.position - transform.position;
    Vector2 DirectionParalaxToPlayer => new Vector3(DirectionToPlayer.y, -DirectionToPlayer.x);

    private void Awake()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsBlocked())
        {
            AvoidObstacle();
        }
        else
            MoveTowardsPlayer();
    }

    bool IsBlocked()
    {
        return Raycast(transform.position, DirectionToPlayer, 1.5f) || Raycast(transform.position, DirectionToPlayer + DirectionParalaxToPlayer, 1.5f) || Raycast(transform.position, DirectionToPlayer + -DirectionParalaxToPlayer, 1.5f);
    }

    void AvoidObstacle()
    {
        bool slightRightIsCloser = SlightRightIsCloser(out bool failed);
        if (failed)
            rigidbody.AddForce(ClampedToMoveSpeed(DirectionToPlayer));
        if (slightRightIsCloser)
            rigidbody.AddForce(ClampedToMoveSpeed(-DirectionParalaxToPlayer));
        else
            rigidbody.AddForce(ClampedToMoveSpeed(DirectionParalaxToPlayer));
        if(!Raycast(transform.position, DirectionToPlayer, 1.5f))
            rigidbody.AddForce(ClampedToMoveSpeed(DirectionToPlayer));
        Debug.Log("avoiding");
        /*
        if(SlightRightIsCloser())
            rigidbody.AddForce(ClampedToMoveSpeed(-DirectionParalaxToPlayer * 2 + DirectionToPlayer));
        else
            rigidbody.AddForce(ClampedToMoveSpeed(DirectionParalaxToPlayer));
        /*
        if((transform.position + transform.right * 30 - player.transform.position).magnitude < (transform.position - transform.right * 30 - player.transform.position).magnitude)
        {
            rigidbody.AddForce(DirectionParalaxToPlayer);
        }
        else
        {
            rigidbody.AddForce(-transform.right);
        }
        */
    }

    bool SlightRightHit()
    {
        return Raycast(transform.position, DirectionToPlayer + DirectionParalaxToPlayer, 2);
    }
    bool SlightLeftHit()
    {
        return Raycast(transform.position, DirectionToPlayer + -DirectionParalaxToPlayer, 2);
    }

    bool SlightRightIsCloser(out bool failed)
    {
        Raycast(transform.position, DirectionToPlayer + DirectionParalaxToPlayer, 1, out float hitR);
        Raycast(transform.position, DirectionToPlayer + -DirectionParalaxToPlayer, 1, out float hitL);

        failed = false;
        if (hitL == float.PositiveInfinity && hitR == float.PositiveInfinity) failed = true;
        else if (hitL == float.PositiveInfinity) return true;
        else if (hitR == float.PositiveInfinity) return false;

        if (hitR < hitL)
        {
            return true;
        }
        return false;
    }

    bool Raycast(Vector2 origin, Vector2 direction, float distance)
    {
        return Raycast(origin, direction, distance, out float hit);
    }
    bool Raycast(Vector2 origin, Vector2 direction, float distance, out float hitDistance)
    {
        foreach (var item in Physics2D.RaycastAll(origin, direction, distance))
        {
            if (item.collider.gameObject != gameObject && item.collider.gameObject.tag != "Player")
            {
                hitDistance = item.distance;
                return true;
            }
        }
        hitDistance = float.PositiveInfinity;
        return false;
    }

    void MoveTowardsPlayer()
    {
        //Get direction to player, limited to move speed
        Vector2 direction = ClampedToMoveSpeed(DirectionToPlayer); ;

        //Move cat towards player
        rigidbody.AddForce(direction);
    }

    Vector2 ClampedToMoveSpeed(Vector2 direction)
    {
        if (direction.magnitude < minSpeed) direction = direction.normalized * minSpeed;
        else if (direction.magnitude > maxSpeed) direction = direction.normalized * maxSpeed;
        return direction;
    }

    private void OnDrawGizmos()
    {
        //Draws a 1 meter line towards the player
        if (player != null)
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y) + DirectionToPlayer.normalized);
    }
}
