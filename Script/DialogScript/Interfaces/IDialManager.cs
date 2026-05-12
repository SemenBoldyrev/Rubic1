using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript.Interfaces
{
    public interface IDialManager
        ///regulates the flow of data
    {
        public event Action<bool> DialogEnded; ///shows how well the dialog ended
        public bool DialogIsPlaying { get; }

        public void StartDialog(string path, int special = 0);
        public void ForseEndDialog();
    }
}
