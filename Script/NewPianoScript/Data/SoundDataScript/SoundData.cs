using Godot;
using Godot.Collections;
using Rubic1.Script.NewPianoScript.Data.SoundDataScript;
using Rubic1.Script.NewPianoScript.Data.SoundDataScript.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Data.CustomSoundScript
{
    [GlobalClass]
    public partial class SoundData : Resource
    {
        // mostly needed data, for sound generation, is stored here, BUT still not connected to audio players
        [Export] Vector2 minMaxVolume = new Vector2(-80,0);

        [Export] SoundAltTypeEnum alternativeType;
        [Export] bool startAlt = false;

        [Export] SoundPlaybackEnum playbackType;

        [Export] Curve ascelerator;

        [Export] Curve pitchLFO;

        [Export] Curve volumeLFO;

        [Export] bool takeMaxFromEnvelope = true;
        [Export] EnvelopSoundData envelopData;

        [Export] AltData altSoundData;



        public float MinVolume => minMaxVolume.X;

        public float MaxVolume => minMaxVolume.Y;

        public float DifVolume => Mathf.Abs(minMaxVolume.Y - minMaxVolume.X);

        public SoundAltTypeEnum AlternativeType => alternativeType;
        // pipec, ja osihsa s malenkoi bukvoi, i cikl mne jaitsa vivorachival!
        public bool StartAlt => startAlt;

        public SoundPlaybackEnum PlaybackType => playbackType;

        public Curve Ascelerator => ascelerator;

        public Curve PitchLFO => pitchLFO;

        public Curve VolumeLFO => volumeLFO;

        public bool TakeMaxFromEnvelop => takeMaxFromEnvelope;
        public EnvelopSoundData EnvelopData => envelopData;

        public AltData AltSoundData => altSoundData;

    }
}
