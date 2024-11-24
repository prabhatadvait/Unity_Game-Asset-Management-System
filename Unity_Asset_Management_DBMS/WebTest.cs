using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    IEnumerator Start()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost/sqlconnect/webtest.php"))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("Response: " + responseText);

                // Assuming the response format is "string\tinteger"
                string[] webResults = responseText.Split('\t');
                if (webResults.Length == 2)
                {
                    Debug.Log(webResults[0]);
                    if (int.TryParse(webResults[1], out int webNumber))
                    {
                        webNumber *= 2;
                        Debug.Log(webNumber);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse the second value as an integer.");
                    }
                }
                else
                {
                    Debug.LogError("Unexpected response format.");
                }
            }
        }
    }
}


