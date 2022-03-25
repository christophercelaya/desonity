using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Desonity
{
    public class Identity
    {
        public string backendURL;

        public string appName;

        public Identity(string appName, string backendUrl)
        {
            backendURL = backendUrl;
            appName = UnityWebRequest.EscapeURL(appName);
        }

        private IEnumerator checkLoggedIn(string keyUrl, string uuid, Action<string> onComplete)
        {
            string postData = "{\"uuid\":\"" + uuid + "\"}";
            int remainingTries = 20;
            while (remainingTries > 0)
            {
                var uwr = new UnityWebRequest(keyUrl, "POST");
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(postData);
                uwr.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
                uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
                uwr.SetRequestHeader("Content-Type", "application/json");

                yield return uwr.SendWebRequest();

                if (uwr.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("Error while waiting for logged in public key\n\n" + uwr.error + "\n\n" + postData);
                }
                else
                {
                    string returnedKey = uwr.downloadHandler.text;
                    if (returnedKey != "" && returnedKey != null)
                    {
                        onComplete?.Invoke(returnedKey);
                        break;
                    }
                }
                remainingTries--;
                Debug.Log("waiting for login (" + remainingTries + ")");
                yield return new WaitForSeconds(3);
            }
            if (remainingTries == 0)
            {
                onComplete?.Invoke("");
            }
        }

        public IEnumerator getLogin(Action<string> onComplete)
        {
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();

            Application.OpenURL(backendURL + "/login/" + myuuidAsString + "?appname=" + appName);
            
            yield return checkLoggedIn(
                keyUrl: backendURL + "/getKey",
                uuid: myuuidAsString,
                onComplete: onComplete);
        }
    }
}
