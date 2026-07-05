using Godot.Collections;
using Rubic1.Script.NewPianoScript.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Interfaces
{
    public interface IPianoPiano
    {
        public Array<PianoKey> PianoKeyArray { get; }
        public PianoKey PianoAltKey { get; }

        public int Index { get; }

        public event Action<int> KeyPressed;
        public event Action<int> KeyReleased;
        public event Action<bool> KeyboardAltKeyStateChanged;

        public void Activate(bool activate = true);
        public bool AnyButtonPressed();
    }
}
