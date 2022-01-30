using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    public DamageType damageType;
    Vector2 lastLocation;
    
    [SerializeField] float rpm = 1;
    float lastLaunch;

    private void OnValidate()
    {
        if(rpm <= 0)
        {
            rpm = 0.001f;
        }
    }

    public Vector2 currentAim = Vector2.up;
    Vector2 CurrentAim
    {
        get
        {
            return currentAim;
        }
        set
        {
            if (Mathf.Abs(value.x) + Mathf.Abs(value.y) == 0) return;
            currentAim = value;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastLaunch > 1/rpm)
        {
            var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.Setup(transform.up, damageType);
        }
    }
    private void FixedUpdate()
    {
        CurrentAim = ((Vector2)transform.position - lastLocation).normalized;
        lastLocation = transform.position;
    }
}
