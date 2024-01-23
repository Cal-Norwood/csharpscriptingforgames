using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector3 velocity;
    private Animator animator;
    public float sprintSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Vector3.zero;
        //line below to mark out
        velocity.x = Input.GetAxisRaw("Horizontal");
        //line below to comment out 
        velocity.y = Input.GetAxisRaw("Vertical");

        if (velocity != Vector3.zero)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.MovePosition(
            transform.position + velocity.normalized * sprintSpeed * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(
            transform.position + velocity.normalized * speed * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", velocity.x);
            animator.SetFloat("moveY", velocity.y);
        }
    }
}
