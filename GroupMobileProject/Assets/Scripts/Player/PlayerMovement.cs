using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] bool isTopDown = true;
    [SerializeField] bool allowKeyControls = true;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Canvas mobileCanvas;
    [SerializeField] Canvas universalCanvas;
    [SerializeField] Canvas pauseCanvas;
    Collider2D collider;
    Rigidbody2D rb2d;
    Animator myAnimator;

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
    }

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
            myAnimator.Play("JumpWithGun", -1, 0f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Disables movement when falling to prevent sticking to walls (as long as player touched the trigger)
        if (collision.gameObject.tag == "FallDeath")
        {
            myAnimator.Play("FallingWithoutHat");
            moveSpeed = 0;
            jumpForce = 0;
            mobileCanvas.enabled = false;
            universalCanvas.enabled = false;
            pauseCanvas.enabled = false;
        }
    }
}
