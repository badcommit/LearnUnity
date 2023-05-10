using System;
using System.Collections.Generic;

namespace Core
{
    public class ChatContext
    {
        public List<Tuple<string, string>> history = new List<Tuple<string, string>>();
        public ChatContext()
        {
        }
        public void Add(string role, string prompt)
        {
            history.Add(new Tuple<string, string>(role, prompt));
        }
    }
}


