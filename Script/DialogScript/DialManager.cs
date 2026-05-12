using Godot;
using Rubic1.Script.DialogScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript
{
    public partial class DialManager : Control, IDialManager
    {
        public bool DialogIsPlaying => throw new NotImplementedException();

        public event Action<bool> DialogEnded;

        // make so interfaces are child nodes loadeed on start

        public void ForseEndDialog()
        {
            throw new NotImplementedException();
        }

        public void StartDialog(string path, int special = 0)
        {
            throw new NotImplementedException();
        }
    }
}
