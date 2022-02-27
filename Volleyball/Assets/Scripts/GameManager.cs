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

    public Timer timer;

    public bool inGame = false;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 9);

        playerScore.text = player.score.ToString();
        opponentScore.text = opponent.score.ToString();
    }

    private void Update()
    {
        if (timer.currentTime < 0)
        {
            inGame = true;
            timer.gameObject.SetActive(false);
        }

        if (ball.position.y < -2.5f && inGame)
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
            inGame = false;
        }
    }
}
