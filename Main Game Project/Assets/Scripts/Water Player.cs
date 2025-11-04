using UnityEngine;

public class WaterPlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] float jumpCooldown = 0.5f;
    private float jumpTimer = 0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (jumpTimer > 0)
            jumpTimer -= Time.deltaTime;

        float moveDir = 0f;
        if (Input.GetKey(KeyCode.A)) moveDir = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir = 1f;

        Move(moveDir);

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

