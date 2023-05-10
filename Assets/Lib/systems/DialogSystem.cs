using System;
using Unity.VisualScripting;
using System.Collections.Generic;
namespace Systems
{
    public class DialogSystem
    {
        private List<Tuple<string, string>> history = new List<Tuple<string, string>>();
        DialogController dialogController;
        static DialogSystem _instance;

        private DialogSystem()
        {

        }

        public static DialogSystem Instance
        {
            get
            {
                _instance ??= new DialogSystem();
                return _instance;
            }
        }

        public void RegisterDialogController(DialogController dialog)
        {
            dialogController = dialog;
        }

        public void Speak(Role.RoleDelegate role, string content)
        {
            history.Add(new Tuple<string, string>(role.GetRoleName(), content));
            if (dialogController != null)
            {
                var template = "";
                for(var i= history.Count -1; i >=0; i--)
                {
                    template += $"{history[i].Item1}: {history[i].Item2}\n";
                }
                dialogController.ShowDialog(template);
            }
        }

        public void Speak(string role, string content)
        {
            history.Add(new Tuple<string, string>(role, content));
            if (dialogController != null)
            {
                var template = "";
                for (var i = history.Count - 1; i >= Math.Max(history.Count - 3, 0); i--)
                {
                    template += $"{history[i].Item1}: {history[i].Item2}\n\n";
                }
                dialogController.ShowDialog(template);
            }
        }

        public void Clear()
        {
            if(dialogController != null)
            {
                dialogController.ClearDialog();
            }
        }

    }
}


