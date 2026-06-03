using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript.DataSets
{
    public struct DialPartData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dial { get; set; }

        public string SpritePath { get; set; }
        public List<int> SpriteSpan { get; set; }
        public List<int> SpriteDimension { get; set; }

        public List<int> Addition { get; set; }

        public int Tag { get; set; }
        public int Next { get; set; }
        public int Event { get; set; }

        public List<string> ChoiceNames { get; set; }
        public List<int> ChoiceCheck { get; set; }
        public List<int> ChoiceNext { get; set; }
        public List<int> ChoiceEvent { get; set; }
    }
}
