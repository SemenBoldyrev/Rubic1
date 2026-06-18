using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Data.SoundDataScript
{
    [GlobalClass]
    public partial class AltData: Resource
    {
        [Export] private Curve pedalEnvelopB;
        [Export] private Curve vibratoLFO;

        public Curve PedalEnvelopB => pedalEnvelopB;
        public Curve VibratoLFO => vibratoLFO;
    }
}
