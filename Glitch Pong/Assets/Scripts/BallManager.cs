using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private GameManager gameManager;

    private AudioSource audioSource;
    public AudioClip ballHitSound;

    private void Start()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GameObject.Find("Game Manager").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameManager.inRound)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        audioSource.PlayOneShot(ballHitSound, PlayerPrefs.GetFloat("sfx volume"));
    }
}
