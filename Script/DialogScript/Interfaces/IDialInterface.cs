using Rubic1.Script.DialogScript.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript.Interfaces
{
    public interface IDialInterface
        ///regulates how everything is shown
    {
        public event Action<int,int> PartEnded;

        public void ShowDial(DialPartData part, bool animation = true);
        public void SkipDial();
        public void ClearDial();
        public void MakeVisible(bool visible = true);
    }
}
