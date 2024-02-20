using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] bool isTopDown = true;
    [SerializeField] bool allowKeyControls = true;
    [SerializeField] bool canJump = true;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Canvas mobileCanvas;
    [SerializeField] Canvas universalCanvas;
    [SerializeField] Canvas pauseCanvas;
    Collider2D playerCollider;
    Rigidbody2D PlayerRB;
    Animator playerAnimator;

    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>(); 
        if (isTopDown)
        {
            PlayerRB.gravityScale = 0;
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
            PlayerRB.velocity = new Vector3(move.x * moveSpeed, move.y * moveSpeed);
        }
        else
        {
            PlayerRB.velocity = new Vector3(move.x * moveSpeed, PlayerRB.velocity.y, 0);
        }
    }

    public void Jump()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && canJump == true)
        {
            PlayerRB.AddForce(new Vector2(0, 20 * jumpForce));
            playerAnimator.Play("JumpWithGun", -1, 0f);
        }
    }

    // Used for running and activating animations
    void Run()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(PlayerRB.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    // Flips Sprite when the character moves left or right
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(PlayerRB.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(-Mathf.Sign(PlayerRB.velocity.x), 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Disables movement when falling to prevent sticking to walls (as long as player touched the trigger) and removes HUD
        if (collision.gameObject.tag == "Falling")
        {
            moveSpeed = 0;
            jumpForce = 0;
            mobileCanvas.enabled = false;
            universalCanvas.enabled = false;
            pauseCanvas.enabled = false;
        }

        // Disables movements when riding a tumbleweed
        if (collision.gameObject.tag == "TooHigh")
        {
            canJump = false;
            moveSpeed = 0;
            jumpForce = 0;
            PlayerRB.velocity = Vector2.zero;
            playerCollider.isTrigger = true; // Player won't get stuck on cactus but will fall through platforms
            mobileCanvas.enabled = false;
            universalCanvas.enabled = false;
            pauseCanvas.enabled = false;
        }
    }
}
