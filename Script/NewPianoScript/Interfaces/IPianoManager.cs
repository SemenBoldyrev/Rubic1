using Rubic1.Script.NewPianoScript.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Interfaces
{
    public interface IPianoManager
    {
        public event Action<NoteData> NotePlayed;
        public event Action<NoteData> NoteStopped;

        public event Action<InstrumentRes> SessionStarted;
        public event Action<InstrumentRes> SessionEnded;

        public bool Active { get; }

        public IPianoKeyboard PianoKeyboard { get; }
        public IPianoSoundPlayer PianoSound {  get; }
        public IPianoInterpretee Interpretee { get; }
        public InstrumentRes CurInstrument { get; }

        public void StartPianoSession(InstrumentRes instrument);

        public void StopPianoSession();
    }
}
