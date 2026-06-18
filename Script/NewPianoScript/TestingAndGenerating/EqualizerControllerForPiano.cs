using Godot;
using Rubic1.Script.NewPianoScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.TestingAndGenerating
{
    public partial class EqualizerControllerForPiano: Control
    {
        //requires it to manipulate the sound
        [Export] Node soundPlayer;

        private IPianoSoundPlayer SoundPlayer;

        public override void _Ready()
        {
            SoundPlayer = (IPianoSoundPlayer)soundPlayer;
        }
    }
}
