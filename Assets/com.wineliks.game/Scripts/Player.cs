using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int jumpCount;
    [SerializeField] float jumpForce;
    private Rigidbody2D Rigidbody { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            jumpCount++;
            if(jumpCount > 1)
            {
                Rigidbody.velocity = Vector2.zero;
            }

            Rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody.gravityScale = Rigidbody.velocity.y < 0 ? 10 : 3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("obstacle"))
        {
            GameManager.Instance.OpenResult();
            return;
        }

        jumpCount = 0;
    }
}
