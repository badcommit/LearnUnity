using System;
using System.Reflection;

namespace Core
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = true,
     AllowMultiple = false)]
    public class DescibeToAI: Attribute
    {
        public string content;
        public DescibeToAI(string content)
        {
            this.content = content;
        }
    }
}


