using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public int damage;
    public ParticleSystem particles;

    private void Start()
    {
        StartCoroutine(Delay());    
    }

    // destroying bullet and playing a particle effect when it hits something that is not the player
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
        // dealing damage if the bullet contacts with an enemy
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

    // if bullet hasn't hit anything destroy after 5 seconds to prevent build up
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
