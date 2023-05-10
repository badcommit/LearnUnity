using System;
using System.Text.RegularExpressions;
using Core;
using UnityEngine;
namespace GameSpecific
{
    public abstract class AINPCDelegate : Role.RoleDelegateBase
    {
        public override void DispatchCall(string function, string parameter)
        {
            Debug.Log($"Dispatched {function} {parameter}");
            if (function == "Talk")
            {
                Talk(parameter);
            }
            else if (function == "Fireball")
            {
                Fireball(parameter);
            }
            else if (function == "Moveleft")
            {
                Moveleft();
            }
            else if (function == "Moveright")
            {
                Moveright();
            }
        }

        public AINPCDelegate(string id): base(id, "Dragon", " He likes to talk with other nice characters like Birdy, when he meets anybody he does not like. he will use fireball. E.g. Dolly.")
        {
            
        }
        [Core.DescibeToAI("Funciton Name Talk, if you want to invoke this function, please use the following format [Role][Talk][Content], where Role is role name of this character, Talk is a fixed keyword and Content is the content you want to say. For example: [Dragon][Talk][Hello, good morning.]")]
        public abstract void Talk(string content);

        [Core.DescibeToAI("Funciton Name Fireball, this is used to let other not approach to you or attack enermy. if you want to invoke this function, please use the following format [Role][Fireball][Content], where Role is role name of this character, Attack is a fixed keyword and Content is either left or right to fire a fireball to left or right. For example: [Dragon][Fireball][right]")]
        public abstract void Fireball(string direction);


        [Core.DescibeToAI("Funciton Name Moveleft, this is used to let other not approach to you. if you want to invoke this function, please use the following format [Role][Moveleft][Content], where Role is role name of this character, Moveleft is a fixed keyword and Content is empty string. For example: [Dragon][Moveleft][ ], tthis will avoid enery from right")]
        public abstract void Moveleft();

        [Core.DescibeToAI("Funciton Name Moveright, this is used to let other not approach to you. if you want to invoke this function, please use the following format [Role][Moveright][Content], where Role is role name of this character, Moveright is a fixed keyword and Content is empty string. For example: [Dragon][Moveright][ ], this will avoid enery from left")]
        public abstract void Moveright();


    }
}


