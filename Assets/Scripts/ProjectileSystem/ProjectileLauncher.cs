using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    public DamageType damageType;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.Setup(transform.up, damageType);
        }
    }
}
