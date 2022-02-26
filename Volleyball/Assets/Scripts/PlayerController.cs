using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController2D controller;
    public GameManager gameManager;

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
        if (!gameManager.gameFinished)
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

        else if (gameManager.gameFinished && controller.m_Grounded)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
    }

    private void FixedUpdate()
    {
        if (!gameManager.gameFinished)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, slowWalk, jump);
            jump = false;
        }
    }
}
