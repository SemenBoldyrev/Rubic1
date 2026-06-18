using Rubic1.Script.NewPianoScript.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Interfaces
{
    public interface IPianoSoundPlayer
    {
        public InstrumentRes CurInstrument { get; set; }

        public void RequestSound(int soundId);
        public void StopSound(int soundId);
        public void AltSound(bool alt);

        public void StopAllSounds();
    }
}
