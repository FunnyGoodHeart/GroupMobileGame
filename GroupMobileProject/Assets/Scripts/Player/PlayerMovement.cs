using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Collider2D collider;
    Rigidbody2D rb2d;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] bool isTopDown = true;
    [SerializeField] bool allowKeyControls = true;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] MainMenu isEasy;
    [SerializeField] MainMenu isNormal;
    [SerializeField] MainMenu isHard;
    [SerializeField] Canvas mobileCanvas;

    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>(); 
        if (isTopDown)
        {
            rb2d.gravityScale = 0;
        }
        else
        {
            joystick.AxisOptions = AxisOptions.Horizontal;
        }
        if (allowKeyControls)
        {
            mobileCanvas.enabled = false;
        }

        if (isEasy == true)
        {
            moveSpeed = 8;
        }

        if (isNormal == true)
        {
            moveSpeed = 6;
        }

        if (isHard == true)
        {
            moveSpeed = 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();


        Vector2 move = new Vector2(0, 0);
        if (allowKeyControls)
        {
            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            move.x = joystick.Horizontal;
            move.y = joystick.Vertical;
        }
        if (isTopDown)
        {
            rb2d.velocity = new Vector3(move.x * moveSpeed, move.y * moveSpeed);
        }
        else
        {
            rb2d.velocity = new Vector3(move.x * moveSpeed, rb2d.velocity.y, 0);
        }
    }

    public void Jump()
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb2d.AddForce(new Vector2(0, 20 * jumpForce));
        }
    }

    // Used for running and activating animations
    void Run()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    // Flips Sprite when the character moves left or right
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(-Mathf.Sign(rb2d.velocity.x), 1f);
        }
    }
}
