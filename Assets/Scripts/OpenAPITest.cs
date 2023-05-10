using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using ChatGPT;

public class OpenAPITest : MonoBehaviour
{
    // Use this for initialization

	void Start()
	{

        //StartCoroutine(ApiTest());
    }

    public void ApiTest()
    {
        Debug.Log("Call me");
        //var request = ChatRequestResponseUtil.GetRequest("how to play games in unity");
        //var webrequest = ChatRequestResponseUtil.GetWebRequest(request);
        //yield return webrequest.SendWebRequest();

        //if (webrequest.result == UnityWebRequest.Result.Success)
        //{
        //    string responseText = webrequest.downloadHandler.text;
        //    Debug.Log("Success");
        //    Debug.Log( responseText);
        //    var response = ChatRequestResponseUtil.FromResponseText(responseText);
        //    Debug.Log($"should get: {response.choices[0].message.content}");
        //}
        //else
        //{
        //    Debug.Log("Error");
        //    Debug.Log(webrequest.error);
        //}

    }

    // Update is called once per frame
    void Update()
	{
	}
}

