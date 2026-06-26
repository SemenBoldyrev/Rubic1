using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Interfaces
{
    public interface IItemFeedback
    {
        public int AskQuestion(string question, List<string> answers =  null);
        public void GiveInfo(string info);
    }
}
