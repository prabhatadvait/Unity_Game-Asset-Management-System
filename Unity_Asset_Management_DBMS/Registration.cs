using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Registration : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("User creation failed. Error: " + www.error);
            }
            else
            {
                if (www.downloadHandler.text == "0")
                {
                    Debug.Log("User created successfully.");
                    SceneManager.LoadScene(0);
                }
                else
                {
                    Debug.Log("User creation failed. Error #" + www.downloadHandler.text);
                }
            }
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}

