using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private Button audioButton;
    private Button quitButton;

    private GameObject audioCanvas;
    private Slider musicSlider;
    private Slider sfxSlider;

    private Button retry;
    private Button menu;

    private GameObject winScreen;
    private GameObject defeatScreen;

    public int playerScore;
    public int opponentScore;

    public bool inRound = false;
    public bool inGame = true;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (audioButton == null || quitButton == null)
            {
                audioButton = GameObject.Find("Audio Button").GetComponent<Button>();
                quitButton = GameObject.Find("Quit Button").GetComponent<Button>();

                audioButton.onClick.AddListener(OpenAudioSettings);
                quitButton.onClick.AddListener(Quit);
            }

            if (audioCanvas == null || musicSlider == null || sfxSlider == null)
            {
                audioCanvas = GameObject.Find("Audio Menu");

                musicSlider = GameObject.Find("Music Slider").GetComponent<Slider>();
                sfxSlider = GameObject.Find("SFX Slider").GetComponent<Slider>();

                audioCanvas.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (winScreen == null || defeatScreen == null)
            {
                winScreen = GameObject.FindGameObjectWithTag("Win Screen");
                defeatScreen = GameObject.FindGameObjectWithTag("Defeat Screen");

                winScreen.SetActive(false);
                defeatScreen.SetActive(false);
            }

            if (playerScore >= 3)
            {
                winScreen.SetActive(true);
                inGame = false;
            }

            else if (opponentScore >= 3)
            {
                defeatScreen.SetActive(true);
                inGame = false;
            }

            else
            {
                inGame = true;
            }

            if (inGame == false && (retry == null || menu == null))
            {
                retry = GameObject.Find("Retry Button").GetComponent<Button>();
                menu = GameObject.Find("Menu Button").GetComponent<Button>();

                retry.onClick.AddListener(Retry);
                menu.onClick.AddListener(Menu);
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenAudioSettings()
    {
        if (audioCanvas.activeSelf == false)
        {
            audioCanvas.SetActive(true);
        }

        else if (audioCanvas.activeSelf == true)
        {
            audioCanvas.SetActive(false);
        }
    }

    public void Retry()
    {
        playerScore = 0;
        opponentScore = 0;
        inRound = false;
        inGame = true;

        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
