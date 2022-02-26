using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public OpponentController opponent;

    public Transform ball;
    public Transform net;

    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI opponentScore;

    public bool gameFinished = false;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
        if (playerScore != null)
        {
            playerScore.text = player.score.ToString();
            opponentScore.text = player.score.ToString();
        }
    }

    private void Update()
    {
        if (ball.position.y < -2.5f && !gameFinished)
        {
            if (ball.position.x < net.position.x)
            {
                opponent.score += 1;
                opponentScore.text = opponent.score.ToString();
            }

            else if (ball.position.x > net.position.x)
            {
                player.score += 1;
                playerScore.text = player.score.ToString();
            }
            gameFinished = true;
        }
    }
}
