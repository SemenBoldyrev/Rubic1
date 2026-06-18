using Rubic1.Script.NewPianoScript.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Interfaces
{
    public interface IPianoKeyboard
    {
        public event Action<int> KeyboardKeyPressed;
        public event Action<int> KeyboardKeyReleased;
        public event Action<bool> KeyboardAltKeyStateChanged;

        public bool AnyKeyPressed { get; }

        public void ShowKeyboard(int id = -1);
    }
}
