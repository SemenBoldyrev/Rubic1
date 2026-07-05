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
using System.Reflection;

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
        public event Action<InstrumentRes> SessionStarted;
        public event Action<InstrumentRes> SessionEnded;

        private bool active = false;

        private int nid = 0;

        public bool Active => active;

        public override void _Ready()
        {
            GD.Print("mama");
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
            nid = 0;

            SessionStarted?.Invoke(curInstrument);
            active = true;
        }

        public void StopPianoSession()
        {
            active = false;
            PianoKeyboard.ShowKeyboard();
            PianoSound.StopAllSounds();

            SessionEnded?.Invoke(curInstrument);
        }

        private void OnAltKeyStateChanged(bool newState)
        {
            if (!Active) return;

            PianoSound.AltSound(newState);
        }

        private void OnKeyboardKeyPressed(int value)
        {
            if (!Active) return;
            GD.Print("a");
            PianoSound.RequestSound(value);
            GD.Print("b");

            NoteData ndt = new NoteData();

            ndt.Id = nid;
            ndt.SoundIntActual = value;
            ndt.SoundInt = Interpretee.GetKeyInt(value);
            ndt.SoundStr = Interpretee.GetKey(value);
            ndt.InstrumentType = curInstrument.InstrumentType;

            NotePlayed?.Invoke(ndt);
        }

        private void OnKeyboardKeyReleased(int value)
        {
            if (!Active) return;

            PianoSound.StopSound(value);
            

            NoteData ndt = new NoteData();

            ndt.Id = nid;
            ndt.SoundIntActual = value;
            ndt.SoundInt = Interpretee.GetKeyInt(value);
            ndt.SoundStr = Interpretee.GetKey(value);
            ndt.InstrumentType = curInstrument.InstrumentType;

            NoteStopped?.Invoke(ndt);
        }
    }
}
