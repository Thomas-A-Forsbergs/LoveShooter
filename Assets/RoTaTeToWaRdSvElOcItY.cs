using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoTaTeToWaRdSvElOcItY : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var diff = rigidbody.velocity;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
