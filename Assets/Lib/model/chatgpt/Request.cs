using System;
using System.Collections.Generic;

namespace ChatGPT.Model
{
    public class Request
    {
        public string model = "gpt-3.5-turbo";
        public List<Message> messages;
        public double temperature;
        public Request()
        {
        }
    }
}


