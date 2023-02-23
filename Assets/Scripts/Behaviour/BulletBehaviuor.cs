using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviuor : MonoBehaviour
{
    public float bulletSpeed = 1;
    public GameObject bulletBody;
    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(bulletBody);
    }
}
