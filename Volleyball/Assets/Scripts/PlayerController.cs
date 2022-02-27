using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController2D controller;
    public GameManager gameManager;

    public Transform ball;

    public float speed = 1f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool slowWalk = false;

    public int score = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (gameManager.inGame)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

            if (Input.GetButtonDown("Crouch"))
            {
                slowWalk = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                slowWalk = false;
            }
        }

        else if (!gameManager.inGame && ball.position.y < -2.5f && controller.m_Grounded)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.inGame)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, slowWalk, jump);
            jump = false;
        }
    }
}
