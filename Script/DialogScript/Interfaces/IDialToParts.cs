using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript.Interfaces
{
    internal interface IDialToParts
        ///translating json into part collections
    {
        public IDialCollection FileToParts(string path);
    }
}
