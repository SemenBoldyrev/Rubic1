using Godot;
using Godot.Collections;
using Rubic1.Script.NewPianoScript.Data;
using Rubic1.Script.NewPianoScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript
{
    public partial class PianoKeyboard : Control, IPianoKeyboard
    {
        public event Action<int> KeyboardKeyPressed;
        public event Action<int> KeyboardKeyReleased;
        public event Action<bool> KeyboardAltKeyStateChanged;

        private IPianoPiano curKeyboard = null;
        private List<IPianoPiano> Keyboards = new List<IPianoPiano>();

        private bool anyKeyPressed = false;
        public bool AnyKeyPressed => anyKeyPressed;

        public override void _Ready()
        {
            Array<Node> childrens =  this.GetChildren();

            for (int i = 0; i<childrens.Count; i++)
            {
                if (childrens[i] is not IPianoPiano) continue;
                IPianoPiano piano = (IPianoPiano)childrens[i];
                Keyboards.Add(piano);

                piano.KeyPressed += OnKeyPress;
                piano.KeyReleased += OnKeyRelease;
                piano.KeyboardAltKeyStateChanged += OnAltKeyStateChanged;

                piano.Activate(false);
            }
        }

        // that basically the same code for PianoPiano, but it works fine and orginised enough
        private void OnAltKeyStateChanged(bool newState)
        {
            KeyboardAltKeyStateChanged?.Invoke(newState);
        }

        private void OnKeyPress(int value)
        {
            anyKeyPressed = true;
            KeyboardKeyPressed?.Invoke(value);
        }

        private void OnKeyRelease(int value)
        {
            anyKeyPressed = curKeyboard.AnyButtonPressed();
            KeyboardKeyReleased?.Invoke(value);
        }

        public void ShowKeyboard(int id = -1)
        {
            if (id < 0)
            {
                if (curKeyboard != null) curKeyboard.Activate(false);
                curKeyboard = null;
                return;
            }
            if (curKeyboard != null) return;

            anyKeyPressed = false;
            curKeyboard = Keyboards[id];
            curKeyboard.Activate(true);
        }
    }
}
