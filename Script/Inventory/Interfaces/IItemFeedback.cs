using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Interfaces
{
    public interface IItemFeedback
    {
        public event Action<int> SelectedChoice;
        public void AskQuestion(List<string> answers, string question = null);
        public void GiveInfo(string info);
    }
}
