using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpStrength = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    [Header("Shooting")]
    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject arrow;
    [SerializeField] float shotingRate = 0.2f;
    [SerializeField] AudioClip shootSound;

    // State
    bool isAlive = true;
    private bool isOnGround = true;

    // Cached component reference
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private CapsuleCollider2D bodyCollider;
    private BoxCollider2D feetCollider;
    private float gravityScaleAtStart;
    private bool canShot = true;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rigidbody2D.gravityScale;
    }

    void Update()
    {
        if (!isAlive) {return;}
        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
        Hit();
        Shoot();
    }

    private void Shoot()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && canShot)
        {
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        canShot = false;
        animator.SetTrigger("shoot");
        Instantiate(arrow, shootPoint.transform.position, shootPoint.transform.rotation);
        yield return new WaitForSeconds(shotingRate);
        canShot = true;
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * speed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rigidbody2D.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClimbing", false);
            return; 
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rigidbody2D.velocity.x, controlThrow * climbSpeed);
        rigidbody2D.velocity = climbVelocity;
        rigidbody2D.gravityScale = 0;

        bool playerHasVerticalSpeed = Mathf.Abs(rigidbody2D.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    private void Jump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpStrength);
            rigidbody2D.velocity += jumpVelocity;
        }
    }
    private void Hit()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            StartCoroutine(ProcessHit());
            if (!GameManager.instance.isAlive)
            {
                Die();
            }
        }
    }

    IEnumerator ProcessHit()
    {
        bodyCollider.enabled = false;
        GetComponent<Rigidbody2D>().velocity = deathKick;
        GameManager.instance.playerHealth--;
        yield return new WaitForSeconds(2);
        bodyCollider.enabled = true;
    }

    private void Die()
    {
        isAlive = false;
        animator.SetTrigger("die");
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);
        }
    }

}
