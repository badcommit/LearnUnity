using System;
using ChatGPT.Model;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;

namespace ChatGPT
{
    public class ChatRequestResponseUtil
  
    {
        readonly static string endpoint = "https://api.openai.com/v1/chat/completions";
        readonly static string secret = Secret.GetSecret();
        public ChatRequestResponseUtil()
        {
        }

        public static UnityWebRequest GetWebRequest(Request request)
        {
            string apiKey = secret;


            UnityWebRequest webrequest = UnityWebRequest.Post(endpoint, "");
            webrequest.SetRequestHeader("Content-Type", "application/json");
            webrequest.SetRequestHeader("Authorization", "Bearer " + apiKey);
            string body = JsonConvert.SerializeObject(request);
            Debug.Log($"{request} body sending");
            webrequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
            webrequest.downloadHandler = new DownloadHandlerBuffer();
            return webrequest;
        }

        public static ChatGPT.Model.Request GetRequest(string prompt, double temp=0.7)
        {
            var request = new ChatGPT.Model.Request();
            request.temperature = temp;
            request.messages = new List<Message>();
            var message = new Message();
            message.role = "user";
            message.content = prompt;
            request.messages.Add(message);
            return request;
        }

        public static Response FromResponseText(string text)
        {
            return JsonConvert.DeserializeObject<Response>(text);
        }
    }
}


