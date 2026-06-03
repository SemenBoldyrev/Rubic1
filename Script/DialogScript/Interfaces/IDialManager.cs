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
        public IDialInterface CurrentDialInterface { get; }
        public IDialCollection CurrentDialCollection { get; }

        public void StartDialogByPath(string path, int dialInterface = 0);
        public void StartDialog(IDialCollection dialCollection, int dialInterface = 0);
        public void ForseEndDialog();
    }
}
