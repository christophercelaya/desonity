using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Desonity
{
    public class Route
    {
        public static string ROUTE = "https://bitclout.com/api/v0";
        public static string getRoute()
        {
            return ROUTE;
        }
        public static void setRoute(string newRoute)
        {
            ROUTE = newRoute;
        }

        public static IEnumerator POST(string endpoint, string postData, Action<string> onComplete)
        {
            var uwr = new UnityWebRequest(ROUTE + endpoint, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(postData);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");

            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error while making post request to " + ROUTE + endpoint + "\n\n" + uwr.error + "\n\n" + postData);
            }
            else
            {
                string JSONStr = uwr.downloadHandler.text;
                onComplete?.Invoke(JSONStr);
            }
        }
    }
}