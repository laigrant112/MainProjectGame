using UnityEngine;

public class WaterPlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] float jumpCooldown = 0.5f;

    private float jumpTimer = 0f;
    private Vector3 originalScale;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // save the starting size
    }

    void Update()
    {
        // cooldown timer
        if (jumpTimer > 0)
            jumpTimer -= Time.deltaTime;

        // movement direction
        float moveDir = 0f;
        if (Input.GetKey(KeyCode.A)) moveDir = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir = 1f;

        Move(moveDir);

        
        if (moveDir < 0) // face right
            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        else if (moveDir > 0) // face left
            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );

        // jump
        if (Input.GetKeyDown(KeyCode.W) && jumpTimer <= 0f)
        {
            Jump();
            jumpTimer = jumpCooldown;
        }
    }

    void Move(float dir)
    {
        Vector2 movementVector = new Vector2(dir, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movementVector);
    }

    void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}


