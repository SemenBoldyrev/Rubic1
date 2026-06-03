using Godot;
using Rubic1.Script.DialogScript.DataSets;
using Rubic1.Script.DialogScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript
{
    public class DialCollection : IDialCollection
    {
        private Dictionary<int, DialPartData> dialDictionary = new Dictionary<int, DialPartData>();
        private List<DialPartData> dialList = new List<DialPartData>();
        private List<DialPartData> orgDialList = new List<DialPartData>();

        public Dictionary<int, DialPartData> DialDictionary => dialDictionary;

        public List<DialPartData> DialList => dialList;

        public List<DialPartData> OriginDialList => orgDialList;

        public void AddPart(DialPartData data)
        {
            orgDialList.Add(data);
            if (data.Id < 0) { return; }
            if (data.Id > dialList.Count-1)
            {
                dialList.Add(data);
                dialDictionary[data.Id] = data;
                return;
            }
            dialList[data.Id] = data;
            dialDictionary[data.Id] = data;
        }

        public DialPartData GetDialPart(int id)
        {
            return dialDictionary[id];
        }

        public void LoadFromList(List<DialPartData> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                AddPart(list[i]);
            }
            orgDialList = list;
        }
    }
}
