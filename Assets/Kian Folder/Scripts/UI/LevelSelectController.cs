using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class LevelSelectController : MonoBehaviour
{
    public Button returnBTN;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        returnBTN = root.Q<Button>("btLSExit");

        returnBTN.clicked += ReturnButtonClicked;

    }

    void ReturnButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
