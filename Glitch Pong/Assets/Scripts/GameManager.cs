using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private Button playButton;
    private Button audioButton;
    private Button quitButton;

    private GameObject audioCanvas;
    private Slider musicSlider;
    private Slider sfxSlider;

    public AudioSource mySounds;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public Animator fadeOutAnim;

    private Button retry;
    private Button menu;

    private GameObject winScreen;
    private GameObject defeatScreen;

    public int playerScore;
    public int opponentScore;

    private bool fadedOut = false;

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

    private void Start()
    {
        if (!PlayerPrefs.HasKey("music volume"))
        {
            PlayerPrefs.SetFloat("music volume", 0.5f);
        }

        if (!PlayerPrefs.HasKey("sfx volume"))
        {
            PlayerPrefs.SetFloat("sfx volume", 0.5f);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (playButton == null || audioButton == null || quitButton == null)
            {
                playButton = GameObject.Find("Play Button").GetComponent<Button>();
                audioButton = GameObject.Find("Audio Button").GetComponent<Button>();
                quitButton = GameObject.Find("Quit Button").GetComponent<Button>();

                playButton.onClick.AddListener(PlayWrapper);
                audioButton.onClick.AddListener(OpenAudioSettings);
                quitButton.onClick.AddListener(QuitWrapper);

                UpdateEventTrigger(playButton);
                UpdateEventTrigger(audioButton);
                UpdateEventTrigger(quitButton);
            }

            if (audioCanvas == null || musicSlider == null || sfxSlider == null)
            {
                audioCanvas = GameObject.Find("Audio Menu");

                musicSlider = GameObject.Find("Music Slider").GetComponent<Slider>();
                sfxSlider = GameObject.Find("SFX Slider").GetComponent<Slider>();

                musicSlider.value = PlayerPrefs.GetFloat("music volume");
                sfxSlider.value = PlayerPrefs.GetFloat("sfx volume");

                musicSlider.onValueChanged.AddListener(delegate { UpdateMusicSetting(); });
                sfxSlider.onValueChanged.AddListener(delegate { UpdateSfxSetting(); });

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
                StartCoroutine(ActivateWinScreen());
            }

            else if (opponentScore >= 3)
            {
                StartCoroutine(ActivateDefeatScreen());
            }

            else
            {
                inGame = true;
            }

            if (inGame == false && (retry == null || menu == null) && fadedOut)
            {
                retry = GameObject.Find("Retry Button").GetComponent<Button>();
                menu = GameObject.Find("Menu Button").GetComponent<Button>();

                retry.onClick.AddListener(RetryWrapper);
                menu.onClick.AddListener(MenuWrapper);

                UpdateEventTrigger(retry);
                UpdateEventTrigger(menu);
            }
        }

        if (fadeOutAnim == null)
        {
            fadeOutAnim = GameObject.Find("Fade Out").GetComponent<Animator>();
        }
    }

    public void PlayWrapper()
    {
        StartCoroutine(Play());
    }

    public IEnumerator Play()
    {
        fadeOutAnim.SetTrigger("Fade Out");
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(1);
        fadeOutAnim.SetTrigger("Fade In");
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

    private void UpdateMusicSetting()
    {
        PlayerPrefs.SetFloat("music volume", musicSlider.value);
    }

    private void UpdateSfxSetting()
    {
        PlayerPrefs.SetFloat("sfx volume", sfxSlider.value);
    }

    private IEnumerator ActivateWinScreen()
    {
        inGame = false;
        yield return new WaitForSeconds(2);

        winScreen.SetActive(true);
        fadedOut = true;
    }

    private IEnumerator ActivateDefeatScreen()
    {
        inGame = false;
        yield return new WaitForSeconds(2);

        defeatScreen.SetActive(true);
        fadedOut = true;
    }

    public void RetryWrapper()
    {
        StartCoroutine(Retry());
    }

    public IEnumerator Retry()
    {
        fadeOutAnim.SetTrigger("Fade Out");

        playerScore = 0;
        opponentScore = 0;
        inRound = false;
        inGame = true;

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(1);
        fadeOutAnim.SetTrigger("Fade In");
        fadedOut = false;
    }

    public void MenuWrapper()
    {
        StartCoroutine(Menu());
    }

    public IEnumerator Menu()
    {
        fadeOutAnim.SetTrigger("Fade Out");

        playerScore = 0;
        opponentScore = 0;
        inRound = false;
        inGame = true;

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(0);
        fadeOutAnim.SetTrigger("Fade In");
        fadedOut = false;
    }

    private void UpdateEventTrigger(Button button)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();

        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener((callback) => { HoverSound(); });
        trigger.triggers.Add(hoverEntry);

        EventTrigger.Entry clickEntry = new EventTrigger.Entry();

        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener((callback) => { ClickSound(); });
        trigger.triggers.Add(clickEntry);
    }

    public void HoverSound()
    {
        mySounds.PlayOneShot(hoverSound, PlayerPrefs.GetFloat("sfx volume"));
    }

    public void ClickSound()
    {
        mySounds.PlayOneShot(clickSound, PlayerPrefs.GetFloat("sfx volume"));
    }

    public void QuitWrapper()
    {
        StartCoroutine(Quit());
    }

    public IEnumerator Quit()
    {
        yield return new WaitForSeconds(2);

        Application.Quit();
    }
}
