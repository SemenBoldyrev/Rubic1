using Rubic1.Script.DialogScript.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript.Interfaces
{
    internal interface IDialCollection
        ///holds the parts of dialog
    {
        public Dictionary<int, DialPartData> DialDiactionary { get; }
        public List<DialPartData> DialList { get; }
        public List<DialPartData> OriginDialList { get; }

        public DialPartData GetDialPart(int id);
        public void AddPart(DialPartData data);
        public void LoadFromList(List<DialPartData> list);
    }
}
