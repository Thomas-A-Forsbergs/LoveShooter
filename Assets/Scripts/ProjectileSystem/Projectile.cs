using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    new Rigidbody2D rigidbody;

    [SerializeField] float velocity = 10;
    [SerializeField] DamageType damageType;

    public void Setup(Vector2 moveDirection, DamageType damageType)
    {
        rigidbody.velocity = moveDirection * velocity;
        AimForwards(moveDirection);
        this.damageType = damageType;
    }

    void AimForwards(Vector2 direction)
    {
        var diff = direction; ;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        //Destroy projectile after 15 seconds
        Destroy(gameObject, 15);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If hitting the person who spawned it, ignore...
        if (collision.tag == "Player" || collision.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast") || collision.isTrigger) return;

        //If hitting a cat, call cat.hit
        var catAI = collision.gameObject.GetComponent<CatAI>();
        if (catAI != null)
        {
            catAI.Hit(damageType);
        }

        PlayerStats.Instance.AddScore(1);

        //Destroy projectile
        Destroy(gameObject);
    }
}
public enum DamageType
{
    Love,
    Water
}
