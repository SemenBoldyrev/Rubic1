using Rubic1.Script.NewPianoScript.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Data
{
    public struct NoteData
    {
        private int id = 0;

        private int soundInt = 0;
        private int soundIntActual = 0;
        private string soundStr = "";

        private InstrumentTypeEnum instrumentType = 0;


        public int Id { get => id; set => id = value; }

        public int SoundInt { get => soundInt; set => soundInt = value; }
        public int SoundIntActual { get => soundIntActual; set => soundIntActual = value; }
        public string SoundStr { get => soundStr; set => soundStr = value; }

        public InstrumentTypeEnum InstrumentType { get => instrumentType; set => instrumentType = value; }

        public NoteData() { }
    }
}
