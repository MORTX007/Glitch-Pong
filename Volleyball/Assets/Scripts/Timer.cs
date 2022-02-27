using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    public float currentTime = 0f;
    private float startingTime = 3f;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        currentTime = startingTime;
        timerText.text = currentTime.ToString();
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0");
    }
}
