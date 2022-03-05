using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutManager : MonoBehaviour
{
    private static FadeOutManager instance;

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
}
