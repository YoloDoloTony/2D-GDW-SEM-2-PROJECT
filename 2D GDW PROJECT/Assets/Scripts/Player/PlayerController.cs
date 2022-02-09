using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    //Player Movement
    public float playerSpeed;
    bool facingRight = true;

    public float dashForce;

    //Gravity Variables
    bool top;
    bool isGrounded;
    bool isVertical;
    bool isRight;

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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("pressed shift");
            Dash();
        }
    }



    //Player Movement
    private void MovePlayer()
    {
        Vector2 movementDir = new Vector2(0.0f, 0.0f);

        //Horizontal player movement
        if (!isVertical)
        {
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
        }
        //Vertical (right) player movement
        if (isVertical && isRight)
        {
            if (Input.GetKey(KeyCode.A))
            {
                movementDir += new Vector2(0.0f, -1.0f);

                if (facingRight)
                {
                    FaceDirection();
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                movementDir += new Vector2(0.0f, 1.0f);

                if (!facingRight)
                {
                    FaceDirection();
                }
            }
        }
        //Vertical (left) player movement
        if (isVertical && !isRight)
        {
            if (Input.GetKey(KeyCode.A))
            {
                movementDir += new Vector2(0.0f, 1.0f);

                if (facingRight)
                {
                    FaceDirection();
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                movementDir += new Vector2(0.0f, -1.0f);

                if (!facingRight)
                {
                    FaceDirection();
                }
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
        //Flip for horizontal
        if (!isVertical)
        {
            if (!top)
            {
                transform.eulerAngles = new Vector3(0, 0, 180f);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }
        }
        //Flip for vertical (right)
        if (isVertical && isRight)
        {
            if (!top)
            {
                transform.eulerAngles = new Vector3(0, 0, -90f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 90f);
            }
        }
        //Flip for vertical (left)
        if (isVertical && !isRight)
        {
            if (!top)
            {
                transform.eulerAngles = new Vector3(0, 0, 90f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, -90f);
            }
        }
        facingRight = !facingRight;
        top = !top;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        
        if (collision.gameObject.CompareTag("platform"))
        {
            transform.parent = collision.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            transform.parent = null;
        }
    }
    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetIsVertical()
    {
        return isVertical;
    }

    public void SetIsVertical(bool vertical)
    {
        isVertical = vertical;
    }

    public void SetIsRight(bool right)
    {
        isRight = right;
    }

    public void Dash()
    {

        Debug.Log("dash");
        //rb.velocity = new Vector2(rb.velocity.x,0f);
        if (facingRight)
        {
            rb.AddForce(transform.right * dashForce, ForceMode2D.Impulse);
        }
        if (!facingRight)
        {
            rb.AddForce(transform.right * dashForce * -1, ForceMode2D.Impulse);
        }

    }
}