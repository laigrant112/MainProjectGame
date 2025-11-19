using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rb2d;

    [SerializeField] float jumpCooldown = 0.5f; // cooldown in seconds
    private float jumpTimer = 0f; // counts down

    private float moveDir = 0f;   // stores horizontal input
    private Vector3 originalScale; // stores starting sprite scale

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // keep initial size for flipping
    }

    // Update is called once per frame
    void Update()
    {
        // Update jump timer
        if (jumpTimer > 0)
            jumpTimer -= Time.deltaTime;

        // get input direction (left/right)
        moveDir = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir = 1f;
        }

        // ----- Sprite flip (no shrinking) -----
        if (moveDir > 0) // moving right → face right
        {
            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
        else if (moveDir < 0) // moving left → face left
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }

        // jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpTimer <= 0f)
        {
            Jump();
            jumpTimer = jumpCooldown; // reset cooldown
        }
    }

    // physics movement happens here for smoother slopes
    void FixedUpdate()
    {
        Move(moveDir);
    }

    void Move(float dir)
    {
        // use Rigidbody2D velocity instead of Translate so slopes are smooth
        Vector2 v = rb2d.velocity;
        v.x = dir * moveSpeed;   // no Time.deltaTime here; velocity is units per second
        rb2d.velocity = v;
    }

    void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
