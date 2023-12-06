using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Xml.XPath;

public class MainMenuController : MonoBehaviour
{
    public Button startGameBTN;
    public Button levelSelectBTN;
    public Button exitGameBTN;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startGameBTN = root.Q<Button>("btStart");
        levelSelectBTN = root.Q<Button>("btLevels");
        exitGameBTN = root.Q<Button>("btExit");

        startGameBTN.clicked += StartButtonPressed;
        levelSelectBTN.clicked += LevelSelectPressed;
        exitGameBTN.clicked += ExitButtonPressed;
    }

    void StartButtonPressed()
    {
        //SceneManager.LoadScene("KianScene");
        SceneManager.LoadScene("FrankensteinsCastle");
    }

    void LevelSelectPressed()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    void ExitButtonPressed()
    {
        Application.Quit();
    }
}
