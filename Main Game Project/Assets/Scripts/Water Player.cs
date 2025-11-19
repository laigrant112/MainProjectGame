using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] float jumpCooldown = 0.5f; // cooldown in seconds

    private float jumpTimer = 0f; // counts down
    private float moveDir = 0f;   // <-- added: stores horizontal movement input

    private Vector3 originalScale;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // keep original sprite size
    }

    void Update()
    {
        // Update jump timer
        if (jumpTimer > 0)
            jumpTimer -= Time.deltaTime;

        // get input direction (left/right)
        moveDir = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveDir = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir = 1f;
        }

        // sprite flip
        if (moveDir < 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveDir > 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        // jump
        if (Input.GetKeyDown(KeyCode.W) && jumpTimer <= 0f)
        {
            Jump();
            jumpTimer = jumpCooldown;
        }
    }

    void FixedUpdate()
    {
        Move(moveDir);
    }

    void Move(float dir)
    {
        // use Rigidbody2D velocity instead of Translate
        Vector2 v = rb2d.velocity;
        v.x = dir * moveSpeed;   // velocity naturally handles slopes
        rb2d.velocity = v;
    }

    void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}



