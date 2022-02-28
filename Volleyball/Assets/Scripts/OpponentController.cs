using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb;

    public Transform ball;
    public Transform net;

    public float optimalDefensePos;
    public float speed = 1f;
    public float jumpForce = 1f;

    public float offset = 0.8f;

    private bool grounded = true;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        
        transform.position = new Vector2 (optimalDefensePos, transform.position.y);
    }

    private void Update()
    {
        if (gameManager.inRound)
        {
            if (transform.position.y > -2.4)
            {
                grounded = false;
            }

            else
            {
                grounded = true;
            }
        }

        else if (!gameManager.inRound && ball.position.y < -2.5f && grounded)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
    }

    private void FixedUpdate()
    {

        if (gameManager.inRound)
        {
            if (ball.position.x > net.position.x + .05f)
            {
                Vector2 a = transform.position;
                Vector2 b = new Vector2(ball.position.x + offset, transform.position.y);
                transform.position = Vector2.MoveTowards(a, b, speed * Time.fixedDeltaTime);

                if (ball.position.y < .4f && Mathf.Abs(ball.position.x - transform.position.x)  < offset + .2f && grounded)
                {
                    rb.AddForce(new Vector2(0f, jumpForce));
                }
            }

            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2 (optimalDefensePos, transform.position.y), 5f * Time.fixedDeltaTime);
            }
        }
    }
}
