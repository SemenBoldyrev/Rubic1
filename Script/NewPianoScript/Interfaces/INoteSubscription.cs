using Rubic1.Script.NewPianoScript.Data;
using Rubic1.Script.NewPianoScript.Data.CustomSoundScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Interfaces
{
    public interface INoteSubscription
    {
        public event Action<InstrumentRes> SessionStarted;
        public event Action<InstrumentRes> SessionEnded;

        public event Action<NoteData> NotePlayed;
        public event Action<NoteData> NoteStopped;

        public event Action<NoteData> NoteSync;

        public List<int> ActualPressed { get; }
        public List<int> IntPressed { get; }
        public List<string> StringPressed { get; }

        public bool AnyKeyPressed { get; }
        }
}
