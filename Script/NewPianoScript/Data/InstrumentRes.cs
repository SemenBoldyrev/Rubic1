using Godot;
using Godot.Collections;
using Rubic1.Script.NewPianoScript.Data.CustomSoundScript;
using Rubic1.Script.NewPianoScript.Data.Enums;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Data
{
    [GlobalClass]
    public partial class InstrumentRes: Resource
    {
        [Export] string name;

        [Export] InstrumentTypeEnum instrumentType;
        [Export] InstrumentKeyboardEnum keyboard;

        [Export] int octave = 4;
        [Export] bool hzBased = true;

        [Export] SoundData soundData;

        [Export] Array<AudioEffect> audioFilters = new();

        public string Name => name;

        public int Octave => octave;

        public InstrumentTypeEnum InstrumentType => instrumentType;
        public InstrumentKeyboardEnum Keyboard => keyboard;

        public bool HzBased => hzBased;

        public SoundData SoundData => soundData;

        public Array<AudioEffect> AudioEffectFilters => audioFilters;
    }
}
