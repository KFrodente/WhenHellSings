using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    public GameObject pauseScreen;

    public bool paused = false;

    private void Awake()
    {
        instance = this;
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckPause();
        }
    }

    public void CheckPause()
    {
        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }

    public void ResetTimeScale()
    {
        paused = false;
        Time.timeScale = 1;
    }
}
