using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] public float movementSpeed;
    [SerializeField] private float jumpSpeed;
    private BoxCollider2D collider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallJumpCooldown;
    [SerializeField] private float gravity;
    [SerializeField] private float upForce;
    [SerializeField] private float awayMultiplier;
    private float horizontalInput;
    private float wallGripCounter;
    private float wallGripCooldown;
    private bool isCoolingDown;
    private float currentFlyingSpeed;
    [SerializeField] private float coyoteTime;
    private float coyoteTimer;

    private Animator animator;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    public bool inPause;
    public bool inEnd;
    private UIManagement management;

    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        wallGripCounter = 0;
        animator = GetComponent<Animator>();
        inPause = false;
        inEnd = false;
        management = FindObjectOfType<UIManagement>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CheckForPauseMenu();
        if (inEnd)
        {
            animator.SetBool("isRunning", false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!inPause)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        
            animator.SetBool("isRunning", horizontalInput != 0 && isGrounded());

            if (isOnWall())
            {
                wallGripCounter += Time.deltaTime;
            }
            else
            {
                wallGripCounter = 0;
            }

            if (isOnWall() && wallGripCounter > 0.6f)
            {
            
                //body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * awayMultiplier * movementSpeed, 0);
                //transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                body.gravityScale = gravity;
                isCoolingDown = true;
                wallGripCounter = 0;
            }

            if (isGrounded())
            {
                isCoolingDown = false;
                animator.SetBool("isGrounded",true);
                coyoteTimer = coyoteTime;
                currentFlyingSpeed = 0f;
            }
            else
            {
                coyoteTimer -= Time.deltaTime;
            }

            //switching direction
            if (horizontalInput > 0.01f)
            {
                transform.localScale = Vector3.one;
            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
        
            //wall jump, movement, jumping
            if (wallJumpCooldown > 0.2f && !isCoolingDown)
            {
                /*if (!isGrounded())
                {
                    if (body.velocity.x < movementSpeed * horizontalInput)
                    {
                        currentFlyingSpeed += 0.5f;
                        body.velocity = new Vector2(horizontalInput * currentFlyingSpeed + body.velocity.x, body.velocity.y);
                    }
                }*/
                /*if (!isGrounded() && Mathf.Abs(horizontalInput) != 0 )
                {
                    body.velocity = new Vector2(horizontalInput * movementSpeed, body.velocity.y);
                }*/
                body.velocity = new Vector2(horizontalInput * movementSpeed, body.velocity.y);
                if (isOnWall() && !isGrounded() && wallGripCooldown <= 0)
                {
                    //body.velocity = Vector2.zero;
                    body.gravityScale = 1f;
                }
                else
                {
                    body.gravityScale = gravity;
                }
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
            }
            else
            {
                wallJumpCooldown += Time.deltaTime;
            }
        }
    }

    private void CheckForPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeInHierarchy && !optionsMenu.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            inPause = true;
            management.PauseMenu();
            gameObject.GetComponent<PlayerAttack>().enabled = false;
            animator.SetBool("isRunning", false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
            inPause = false;
            gameObject.GetComponent<PlayerAttack>().enabled = true;
        }
    }

    private void Jump()
    {
        if (coyoteTimer < 0 && !isOnWall())
            return;
        
        if (isOnWall() && !isGrounded())
        {
            SoundManagement.instance.PlaySound(jumpSound);
            currentFlyingSpeed = -Mathf.Sign(transform.localScale.x) * awayMultiplier;
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * awayMultiplier, upForce);
            wallJumpCooldown = 0;
        }
        else if (isGrounded())
        {
            SoundManagement.instance.PlaySound(jumpSound);
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            animator.SetTrigger("jump");
        }
        else if (coyoteTimer > 0)
        {
            SoundManagement.instance.PlaySound(jumpSound);
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            animator.SetTrigger("jump");
        }
        coyoteTimer = 0;
    }

    public bool isGrounded()
    {
        var hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        var hit2 = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, wallLayer);
        return hit.collider != null || hit2.collider != null;
    }

    private bool isOnWall()
    {
        var hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return hit.collider != null;
    }

    public bool canAttack()
    {
        return isGrounded() && !isOnWall();
    }
}
