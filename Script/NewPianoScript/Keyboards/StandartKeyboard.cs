using Godot;
using Godot.Collections;
using Rubic1.Script.NewPianoScript.Interfaces;
using Rubic1.Script.NewPianoScript.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Keyboards
{
    public partial class StandartKeyboard : Control, IPianoPiano
    {
        [Export] Control pianoKeyContainer;
        [Export] PianoKey altKey;

        private Array<PianoKey> keys = new();

        public Array<PianoKey> PianoKeyArray => keys;
        public PianoKey PianoAltKey => altKey;

        public event Action<int> KeyPressed;
        public event Action<int> KeyReleased;
        public event Action<bool> KeyboardAltKeyStateChanged;

        public override void _Ready()
        {
            Array<Node> chd = pianoKeyContainer.GetChildren();
            for (int i = 0; i < chd.Count; i++)
            {
                if (chd[i] is PianoKey)
                {
                    PianoKey key = (PianoKey)chd[i];
                    keys.Add(key);
                    key.PianoKeyPressed += OnKeyPress;
                    key.PianoKeyReleased += OnKeyRelease;
                }
            }
            altKey.PianoKeyPressed += (value) => { OnAltKeyPressRelease(altKey.ButtonPressed); };
            // its send signal as normal piano key so that should be here too
            altKey.PianoKeyReleased += (value) => { OnAltKeyPressRelease(altKey.ButtonPressed); };
        }

        private void OnAltKeyPressRelease(bool pressed)
        {
            KeyboardAltKeyStateChanged?.Invoke(pressed);
        }

        private void OnKeyPress(int value)
        {
            KeyPressed?.Invoke(value);
        }

        private void OnKeyRelease(int value)
        {
            KeyReleased?.Invoke(value);
        }

        public void Activate(bool activate = true)
        {
            this.ProcessMode = activate ? ProcessModeEnum.Pausable : ProcessModeEnum.Disabled;
            this.Visible = activate;
            if (!activate)
            {
                PianoAltKey.ButtonPressed = false;
                for (int i = 0; i < keys.Count; i++) keys[i].ButtonPressed = false;
            }
        }

        public bool AnyButtonPressed()
        {
            return PianoKeyArray.Any(btn => btn.ButtonPressed);
        }
    }
}
