using Godot;
using Rubic1.Script.NewPianoScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript
{
    public class StandartKeyInterpretee : IPianoInterpretee
    {
        private List<string> keys = new List<string>() {
        "C",
        "?",
        "D",
        "?",
        "E",
        "F",
        "?",
        "G",
        "?",
        "A",
        "?",
        "B"
        };

        public string GetKey(int key, bool sharp = true)
        {
            key = GetKeyInt(key);
            string aKey = keys[key];

            if (aKey == "?")
            {
                if (sharp) aKey = keys[StandalizeInt(key-1)] + "#";
                if (!sharp) aKey = keys[StandalizeInt(key + 1)] + "b";
            }
            return aKey;
        }

        public int GetKeyActualInt(int key)
        {
            return key;
        }

        public int GetKeyInt(int key)
        {
            key = StandalizeInt(key);
            return key;
        }

        private int StandalizeInt(int key)
        {
            if (key < 0) while (key < 0) key += keys.Count;
            if (key >= keys.Count)
            {
                int range = (key / keys.Count);
                for (int i = 0; i < range; i++) key -= keys.Count;
            }
            return key;
        }

    }
}
