using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;

namespace Systems
{
    public class AISystem
    {
        static Regex r = new Regex(@"\[(?<A>[A-Za-z]+)\]\[(?<B>[A-Za-z]+)\]\[(?<C>.*)\]");
        private List<Role.RoleDelegate> roles = new List<Role.RoleDelegate>();
        private GameSpecific.Story.BriefStory briefStory = new GameSpecific.Story.BriefStory();
        static AISystem _instance;
        private ConcurrentQueue<string> to_send_messages = new ConcurrentQueue<string>();
        private ConcurrentQueue<ChatGPT.Model.Response> received_messages = new ConcurrentQueue<ChatGPT.Model.Response>();
        private Core.ChatContext chatContext = new Core.ChatContext();
        private AISystem()
        {
        }

        public static AISystem Instance
        {
            get
            {
                _instance ??= new AISystem();
                return _instance;
            }
        }

        public void RegisterRole(Role.RoleDelegate role)
        {
            Debug.Log($"register {role.GetRoleName()}");
            roles.Add(role);
        }

        private void PrepareGameStart()
        {
            var allDesc = new List<string>();
            allDesc.Add(briefStory.description);
            allDesc.Add(briefStory.roleIntro);
            foreach (var role in roles)
            {

                var des = role.GetWholeDescribe();
                allDesc.Add(des);
            }
            allDesc.Add(briefStory.ending);
            var message = string.Join(".", allDesc);
            to_send_messages.Enqueue(message);
        }

        public async Task StartRecving()
        {
            while (true)
            {
                while (!received_messages.IsEmpty)
                {
                    ChatGPT.Model.Response response;
                    received_messages.TryDequeue(out response);
                    Debug.Log($"haha response {response}");
                    chatContext.Add(response.choices[0].message.role, response.choices[0].message.content);
                    TranslateResponse(response);
                }
                await Task.Delay(10);

            }
        }

        public async Task StartSending()
        {
            PrepareGameStart();
            var count = 0;
            while (true)
            {
                while (!to_send_messages.IsEmpty)
                {
                    count += 1;
                    string request;
                    to_send_messages.TryDequeue(out request);
                    var response = await ChatGPT.ChatRequestResponseUtil.SendRequest(chatContext, request);
                    chatContext.Add("user", request);
                    received_messages.Enqueue(response);
                }

                await Task.Delay(3000);
                

            }


        }

        public void EnqueueMessage(string content)
        {
            to_send_messages.Enqueue(content);
        }

        public void TranslateResponse(ChatGPT.Model.Response response)
        {
            
            var content = response.choices[0].message.content;
            var matches = r.Match(content);
            if (matches.Success)
            {
                var roleName = matches.Groups["A"].Value;
                var function = matches.Groups["B"].Value;
                var parameter = matches.Groups["C"].Value;
                Debug.Log($"Got Matches {roleName} {function} {parameter}");
                foreach (var role in roles){
                    Debug.Log($"iterate through role {role.GetRoleName()}");
                    if (role.GetRoleName() == roleName)
                    {
                        Debug.Log($"should dispatch to function {role.GetRoleName()}");
                        role.DispatchCall(function, parameter);
                    }
                }
            }
        }

    }
}


