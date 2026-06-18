using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Interfaces
{
    public interface IPianoInterpretee
    {
        public string GetKey(int key, bool sharp = true);
        public int GetKeyInt(int key);
        public int GetKeyActualInt(int key);
    }
}
