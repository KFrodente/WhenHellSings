using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseScreenManager : MonoBehaviour
{
    public Button resumeBTN;
    public Button restartBTN;
    public Button exitBTN;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        resumeBTN = root.Q<Button>("ResumeButton");
        restartBTN = root.Q<Button>("RestartButton");
        exitBTN = root.Q<Button>("ExitButton");

        resumeBTN.clicked += ResumeButtonClicked;
        restartBTN.clicked += RestartButtonClicked;
        exitBTN.clicked += ExitButtonClicked;
    }

    void ResumeButtonClicked()
    {
        PauseManager.instance.CheckPause();
        PauseManager.instance.ResetTimeScale();
    }

    void RestartButtonClicked()
    {
        //SceneManager.LoadScene("KianScene");
        SceneManager.LoadScene("FrankensteinsCastle");
        PauseManager.instance.ResetTimeScale();
    }

    void ExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
        PauseManager.instance.ResetTimeScale();

    }
}
