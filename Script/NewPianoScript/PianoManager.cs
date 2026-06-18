using Godot;
using Rubic1.Script.NewPianoScript.Data;
using Rubic1.Script.NewPianoScript.Interfaces;
using Rubic1.Script.NewPianoScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot.Collections;
using Rubic1.Script.Managers;

namespace Rubic1.Script.NewPianoScript
{
    public partial class PianoManager : Node, IPianoManager
    {
        [Export] Node Keyboards;
        [Export] Node SoundPlayer;

        private IPianoInterpretee interpretee = new StandartKeyInterpretee();
        private InstrumentRes curInstrument = null;

        public IPianoKeyboard PianoKeyboard => (IPianoKeyboard)Keyboards;
        public IPianoSoundPlayer PianoSound => (IPianoSoundPlayer)SoundPlayer;
        public IPianoInterpretee Interpretee => interpretee;
        public InstrumentRes CurInstrument => curInstrument;


        public event Action<NoteData> NotePlayed;
        public event Action<NoteData> NoteStopped;
        public event Action SessionStarted;
        public event Action SessionEnded;

        private bool active = false;
        public bool Active => active;

        public override void _Ready()
        {
            ManagerBus.PianoManager = this;

            if (Keyboards is IPianoKeyboard)
            {
                IPianoKeyboard keyboardSaver = (IPianoKeyboard)Keyboards;
                keyboardSaver.KeyboardKeyPressed += OnKeyboardKeyPressed;
                keyboardSaver.KeyboardKeyReleased += OnKeyboardKeyReleased;
                keyboardSaver.KeyboardAltKeyStateChanged += OnAltKeyStateChanged;
            }
        }

        public void StartPianoSession(InstrumentRes instrument)
        {
            curInstrument = instrument;
            PianoKeyboard.ShowKeyboard(((int)curInstrument.Keyboard));
            PianoSound.CurInstrument = CurInstrument;
            active = true;
        }

        public void StopPianoSession()
        {
            active = false;
            PianoKeyboard.ShowKeyboard();
            PianoSound.StopAllSounds();
        }

        private void OnAltKeyStateChanged(bool newState)
        {
            if (!Active) return;

            PianoSound.AltSound(newState);
        }

        private void OnKeyboardKeyPressed(int value)
        {
            if (!Active) return;

            PianoSound.RequestSound(value);
            GD.Print(Interpretee.GetKey(value), " Pressed");
        }

        private void OnKeyboardKeyReleased(int value)
        {
            if (!Active) return;

            PianoSound.StopSound(value);
            GD.Print(Interpretee.GetKey(value), " Released");
        }
    }
}
