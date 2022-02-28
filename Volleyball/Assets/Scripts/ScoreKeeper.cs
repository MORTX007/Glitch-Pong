using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    private GameManager gameManager;
    public PlayerController player;
    public OpponentController opponent;

    public Transform ball;
    public Transform net;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI opponentScoreText;

    public Timer timer;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        Physics2D.IgnoreLayerCollision(8, 9);

        playerScoreText.text = gameManager.playerScore.ToString();
        opponentScoreText.text = gameManager.opponentScore.ToString();
    }

    private void Update()
    {
        if (timer.currentTime < 0 || !gameManager.inGame)
        {
            gameManager.inRound = true;
            timer.gameObject.SetActive(false);
        }

        if (ball.position.y < -2.5f && gameManager.inRound && gameManager.inGame)
        {
            if (ball.position.x < net.position.x)
            {
                gameManager.opponentScore += 1;
                opponentScoreText.text = gameManager.opponentScore.ToString();
            }

            else if (ball.position.x > net.position.x)
            {
                gameManager.playerScore += 1;
                playerScoreText.text = gameManager.playerScore.ToString();
            }
            gameManager.inRound = false;

            if (gameManager.inGame)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
