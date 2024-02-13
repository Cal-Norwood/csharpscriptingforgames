using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public int damage;
    public ParticleSystem particles;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag != "Player")
        {
            particles.Play();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.BroadcastMessage("TakeDamage", damage);
            particles.Play();
            Destroy(gameObject);
        }

        if(collision.tag != "Player" || collision)
        {
            particles.Play();
            Destroy(gameObject);
        }
    }
}
