using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Data.SoundDataScript
{
    [GlobalClass]
    public partial class EnvelopSoundData: Resource
    {
        [Export] Curve envelopA;

        [Export] Curve envelopB;


        public Curve EnvelopA => envelopA;

        public Curve EnvelopB => envelopB;
    }
}
