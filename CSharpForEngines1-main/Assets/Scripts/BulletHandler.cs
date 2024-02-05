using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.BroadcastMessage("TakeDamage", damage);
            Destroy(gameObject);
        }

        if(collision.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
