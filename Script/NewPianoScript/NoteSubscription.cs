using Godot;
using Rubic1.Script.Managers;
using Rubic1.Script.NewPianoScript.Data;
using Rubic1.Script.NewPianoScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript
{
    public partial class NoteSubscription : Node, INoteSubscription
    {
        //This script is needed to hold connection between piano and other world, at least seems that i need it

        private List<int> actualList = new List<int>();
        private Dictionary<int, int> intListDict = new();
        private Dictionary<string, int> stringListDict = new();

        public List<int> ActualPressed => actualList;
        public List<int> IntPressed => intListDict.Keys.ToList();
        public List<string> StringPressed => stringListDict.Keys.ToList();
        public bool AnyKeyPressed => actualList.Count > 0;



        public event Action<NoteData> NotePlayed;
        public event Action<NoteData> NoteStopped;
        public event Action<NoteData> NoteSync;

        public event Action<InstrumentRes> SessionStarted;
        public event Action<InstrumentRes> SessionEnded;

        //Needs to be instantiated after the piano manager, so it placed in the strange places, find it if you can ;)

        public override void _Ready()
        {
            GD.Print("mia");

            ManagerBus.PianoManager.SessionStarted += OnSessionStart;
            ManagerBus.PianoManager.SessionEnded += OnSessionEnd;

            ManagerBus.PianoManager.NotePlayed += OnNotePlayed;
            ManagerBus.PianoManager.NoteStopped += OnNoteStopped;
            
            ManagerBus.NoteSubscription = this;
        }

        private void OnNotePlayed(NoteData note)
        {
            actualList.Add(note.SoundIntActual);
            if (intListDict.Keys.Contains(note.SoundInt))
            {
                intListDict[note.SoundInt] += 1;
                stringListDict[note.SoundStr] += 1;
            }
            else
            {
                intListDict.Add(note.SoundInt, 1);
                stringListDict.Add(note.SoundStr, 1);
            }

            NotePlayed?.Invoke(note);
            GD.Print($"Note PLAYED: {note.Id}, {note.InstrumentType}, {note.SoundStr}, {note.SoundIntActual}");
        }

        private void OnNoteStopped(NoteData note)
        {
            //to preform this, it should work better than clocks
            actualList.Remove(note.SoundIntActual);
            intListDict[note.SoundInt] -= 1;
            stringListDict[note.SoundStr] -= 1;
            if (intListDict[note.SoundInt] <= 0)
            {
                intListDict.Remove(note.SoundInt);
                stringListDict.Remove(note.SoundStr);
            }

            NoteStopped?.Invoke(note);
            GD.Print($"Note STOPPED: {note.Id}, {note.InstrumentType}, {note.SoundStr}, {note.SoundIntActual}");
        }

        private void OnSessionStart(InstrumentRes instrument)
        {
            actualList.Clear();
            intListDict.Clear();
            stringListDict.Clear();

            SessionStarted?.Invoke(instrument);
            GD.Print($"Piano session STARTED: {instrument.Name}");
        }

        private void OnSessionEnd(InstrumentRes instrument)
        {   
            SessionEnded?.Invoke(instrument);
            GD.Print($"Piano session ENDED: {instrument.Name}");
        }
    }
}
