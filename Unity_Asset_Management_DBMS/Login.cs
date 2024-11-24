using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("User login failed. Error: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            Debug.Log("Response Text: " + responseText);  // Log the full response text

            if (!string.IsNullOrEmpty(responseText) && responseText[0] == '0')
            {
                string[] splitResponse = responseText.Split('\t');
                if (splitResponse.Length > 1)
                {
                    DBmanager.username = nameField.text;
                    DBmanager.score = int.Parse(splitResponse[1]);
                    SceneManager.LoadScene(0); // Ensure this is the correct scene index or use scene name
                }
                else
                {
                    Debug.LogError("Unexpected response format: " + responseText);
                }
            }
            else
            {
                Debug.LogError("User login failed. Error No.: " + responseText);
            }
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}
