using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Mainmain : MonoBehaviour
{
    public Button registerButton;
    public Button loginButton;
    public Button playButton;
    public TextMeshProUGUI playerDisplay;

    private void Start()
    {
        if (DBmanager.LoggedIn)
        {
            playerDisplay.text = "Player: " + DBmanager.username;
        }

        registerButton.interactable = !DBmanager.LoggedIn;
        loginButton.interactable = !DBmanager.LoggedIn;
        playButton.interactable = DBmanager.LoggedIn;
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(3);
    }
}

