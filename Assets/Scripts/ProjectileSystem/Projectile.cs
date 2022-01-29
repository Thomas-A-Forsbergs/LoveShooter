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
        this.damageType = damageType;
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
        if (collision.tag == "Player" || collision.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast")) return;

        //If hitting a cat, call cat.hit
        var catAI = collision.gameObject.GetComponent<CatAI>();
        if (catAI != null)
        {
            catAI.Hit(damageType);
        }

        //Destroy projectile
        Destroy(gameObject);
    }
}
public enum DamageType
{
    Love,
    Water
}
