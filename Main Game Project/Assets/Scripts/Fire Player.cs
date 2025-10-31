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

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update jump timer
        if (jumpTimer > 0)
            jumpTimer -= Time.deltaTime;

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Move();
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            Move();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpTimer <= 0f)
        {
            Jump();
            jumpTimer = jumpCooldown; // reset cooldown
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        Vector2 movementVector = new Vector2(x, 0) * Time.deltaTime * moveSpeed;
        transform.Translate(movementVector);
    }

    void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
