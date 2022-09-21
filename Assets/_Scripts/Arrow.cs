using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private Player player;

    [SerializeField] float lauchForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Player>();

        rigidbody2D.velocity = new Vector2(lauchForce * player.transform.localScale.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground", "Enemy")))
        {
            Destroy(gameObject);
        }
    }
}
