using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    public TextMeshProUGUI playerDisplay;
    public TextMeshProUGUI scoreDisplay;
    
    public void Awake()
    {
        if(DBmanager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        playerDisplay.text = "Player: " + DBmanager.username;
        scoreDisplay.text = "Score: " + DBmanager.score;
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBmanager.username);
        form.AddField("score", DBmanager.score);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;
        if(www.text == "0")
        {
            Debug.Log("Game Saved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }

        DBmanager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);  
    }

    public void IncreaseScore() {
        DBmanager.score++;
        scoreDisplay.text = "Score: " + DBmanager.score;
    }
}
