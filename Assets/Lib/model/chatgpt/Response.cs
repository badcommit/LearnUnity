using System;
using  Newtonsoft.Json;
using System.Collections.Generic;

//"""
//{"id":"chatcmpl-74uWQHwdnTd9gwaJa2ZPyA2ZwG5JH","object":"chat.completion","created":1681404938,"model":"gpt-3.5-turbo-0301","usage":{"prompt_tokens":19,"completion_tokens":13,"total_tokens":32},"choices":[{"message":{"role":"assistant","content":"Sure, what do you need help with in your Unity project?"},"finish_reason":"stop","index":0}]}
//"""

namespace ChatGPT.Model
{
    public class Response
    {
        public string id;
        public string model;
        public List<Choice> choices;

        public Response()
        {

        }

    }
}


