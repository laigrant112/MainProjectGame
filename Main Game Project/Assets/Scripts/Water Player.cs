using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlsyer : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
        Move();
        }
           if(Input.GetKey(KeyCode.D))
        {
        Move();
        }
        if (Input.GetKeyDown(KeyCode.W)){
            Jump();
            
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
