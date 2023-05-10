using System;
using Unity.Burst.CompilerServices;
using UnityEngine;
namespace GameSpecific
{
    public class AINPCImpl : GameSpecific.AINPCDelegate
    {
        private Systems.DialogSystem dialogSystems;
        private PlayerController playerController;
        
        public AINPCImpl(Systems.DialogSystem dialogSystem, PlayerController player) : base("")
        {
            this.dialogSystems = dialogSystem;
            this.playerController = player;
        }



        public override void Talk(string content)
        {
            dialogSystems.Speak(this, content);
        }

        public override void Fireball(string direction)
        {

            if(direction.ToLower() == "left")
            {
                playerController.FireLeft();
            } else if(direction.ToLower() == "right")
            {
                playerController.FireRight();

            }
            

        }

        public override void Moveleft()
        {

            playerController.MoveLeft();
        }

        public override void Moveright()
        {

            playerController.MoveRight();
        }
    }

}

