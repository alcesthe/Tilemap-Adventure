using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float health = 3;
    Rigidbody2D rigidbody2D;
    Collider2D bodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            rigidbody2D.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)), 1f);
    }

    public void Hit()
    {
        health--;
        Debug.Log(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
