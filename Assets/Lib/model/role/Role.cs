using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Role
{
    public interface RoleDelegate
    {
        string GetRoleName();
        string GetDescription();
        string GetId();
        void DispatchCall(string function, string parameter);
        string GetWholeDescribe();
    }


    public abstract class RoleDelegateBase: RoleDelegate
    {

        string name;
        string description;
        string id;

        public RoleDelegateBase(string id, string name, string description)
        {
            this.name = name;
            this.id = id;
            this.description = description;
        }

        public string GetDescription()
        {
            return this.description;
        }

        public string GetId()
        {
            return id;
        }

        public string GetRoleName()
        {
            return name;
        }

        public string GetWholeDescribe()
        {
            var strings = ListDescriptionFor(this);
            var function = String.Join(".", strings);
            var ret = $"This role's name is {GetRoleName()}. {GetDescription()}. You can control this role by following functions. {function}";
            Debug.Log($"{ret}");
            return ret;
        }



        public static List<string> ListDescriptionFor(RoleDelegate role)
        {
            Debug.Log($"{role.GetType()}");
            var customeType = typeof(Core.DescibeToAI);
            var methods = role.GetType().GetMethods();
            var ret = new List<string>();
            foreach(var method in methods)
            {

                var attrs = method.GetCustomAttributes(customeType, true);
                foreach(var attr in attrs)
                {
                    var describer = (Core.DescibeToAI)attr;
                    ret.Add(describer.content);
                }
                
            }
            return ret;
        }

        public abstract void DispatchCall(string function, string parameter);
    }
}


