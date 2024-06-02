using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private AudioClip jumpingSound;
    private float wallJumpCooldown;
    private float horizontalInput;
    [SerializeField] private float jumpPower;
    private bool isWallHanging = false;
    private int maxJumps = 2;
    private int jumpsRemaining;
    AudioSource audioSource;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        jumpsRemaining = maxJumps;
    }
    private void Start()
    {
        Debug.Log("Player log");
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f)
            transform.eulerAngles = Vector3.zero;
        else if (horizontalInput < -0.1f)
            transform.eulerAngles = new Vector3(0, 180, 0);

        if (wallJumpCooldown < 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                isWallHanging = true;
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
                anim.SetBool("Wall", true);
            }
            else
            {
                isWallHanging = false;
                body.gravityScale = 1f;
                anim.SetBool("Wall", false);
            }
        }
        else
            wallJumpCooldown += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            jump();

        if (isWallHanging)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                jump();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
                jump();

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

    }

    private void jump()
    {
        if (isGrounded())
        {
            jumpsRemaining = maxJumps;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            audioSource.PlayOneShot(jumpingSound);
        }
        else if (isWallHanging)
        {
            body.velocity = new Vector2(horizontalInput * speed, jumpPower);
            isWallHanging = false;
            anim.SetTrigger("jump");

        }

        else if (jumpsRemaining > 0)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            jumpsRemaining--;

        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWallHanging = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWallHanging = false;
        }
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
