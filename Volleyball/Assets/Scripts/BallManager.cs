using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.inRound)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
