using System;
using ChatGPT.Model;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChatGPT
{
    public class ChatRequestResponseUtil
  
    {
        readonly static string endpoint = "https://api.openai.com/v1/chat/completions";
        readonly static string secret = Secret.GetSecret();
        private ChatRequestResponseUtil()
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

            webrequest.disposeUploadHandlerOnDispose = true;
            webrequest.disposeDownloadHandlerOnDispose = true;
            return webrequest;
        }

        public static ChatGPT.Model.Request GetRequest(string prompt, Core.ChatContext context, double temp=0.7)
        {
            var request = new ChatGPT.Model.Request();
            request.messages = new List<Message>();
            foreach(var history in context.history)
            {
                var m = new Message();
                m.role = history.Item1;
                m.content = history.Item2;
                request.messages.Add(m);
            }
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

        public static async Task<Response> SendRequest(Core.ChatContext context, string prompt)
        {
            Debug.Log($"Sending Request {prompt}");
            var request = ChatRequestResponseUtil.GetRequest(prompt, context);
            var webrequest = ChatRequestResponseUtil.GetWebRequest(request);
            
            await webrequest.SendWebRequest();
           

            if (webrequest.result == UnityWebRequest.Result.Success)
            {
                string responseText = webrequest.downloadHandler.text;

                var response = ChatRequestResponseUtil.FromResponseText(responseText);
                
                Debug.Log($"should get: {response.choices[0].message.content}");
                webrequest.disposeCertificateHandlerOnDispose = true;
                webrequest.disposeDownloadHandlerOnDispose = true;
                return response;
            }
            else
            {
                Debug.Log("Error!!!!!!");
                throw new Exception($"Error: {webrequest.error}");
            }

           
        }
    }
}


