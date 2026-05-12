using Rubic1.Script.DialogScript.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript.Interfaces
{
    internal interface IDialInterface
        ///regulates how everything is shown
    {
        public event Action<int> SelectedChoiceId;
        public void ShowDial(DialPartData part, bool animation = true);
        public void ClearDial();
    }
}
