using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void Update()
    {
        if (gameManager.inGame)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
