using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController2D controller;

    public float speed = 1f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool slowWalk = false;

    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    private void Update()
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
        else  if (Input.GetButtonUp("Crouch"))
        {
            slowWalk = false;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, slowWalk, jump);
        jump = false;
    }
}
