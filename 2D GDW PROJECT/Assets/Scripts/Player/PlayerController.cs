using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    //Player Movement
    public float playerSpeed;
    bool facingRight = true;

    //Gravity Variables
    bool top;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePlayer();

        //Check for Switch Gravity Input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            SwitchGravity();
        }
    }

    //Player Movement
    private void MovePlayer()
    {
        Vector2 movementDir = new Vector2(0.0f, 0.0f);

        if (Input.GetKey(KeyCode.A))
        {
            movementDir += new Vector2(-1.0f, 0.0f);

            if (facingRight)
            {
                FaceDirection();
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDir += new Vector2(1.0f, 0.0f);

            if (!facingRight)
            {
                FaceDirection();
            }
        }

        rb.velocity = movementDir * (playerSpeed);
    }

    //Switches what way player is facing
    private void FaceDirection()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    //180 degree Gravity Switch
    private void SwitchGravity()
    {
        rb.gravityScale *= -1;
        Flip();

        isGrounded = false;
    }

    //Flip Player when they are on the roof
    void Flip()
    {
        if (!top)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        facingRight = !facingRight;
        top = !top;
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if Player is touching roof or floor
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0 || collision.contacts[i].normal.y < 0)
            {
                isGrounded = true;
            }
        }
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

}
