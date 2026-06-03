using Rubic1.Script.DialogScript.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript.Interfaces
{
    public interface IDialToParts
        ///translating json into part collections
    {
        public Task<List<DialPartData>> FileToListAsync(string path);
        public List<DialPartData> FileToList(string path);

    }
}
