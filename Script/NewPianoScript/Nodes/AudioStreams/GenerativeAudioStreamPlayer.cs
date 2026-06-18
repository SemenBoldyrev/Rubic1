using Godot;
using Rubic1.Script.NewPianoScript.Data;
using Rubic1.Script.NewPianoScript.Data.CustomSoundScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Nodes.AudioStreams
{
    [GlobalClass]
    public partial class GenerativeAudioStreamPlayer: AudioStreamPlayer
    {
        //
         // Will hold the AudioStreamGeneratorPlayback.
        private float sampleHz;
        private float pulseHz = 440.0f; // The frequency of the sound wave.
        private float phase = 0.0f;
        //

        private AudioStreamGeneratorPlayback playback;
        private AudioStreamGenerator generator;

        private bool active = false;

        private SoundData curSoundData;

        private bool bufferPartFinished = true;

        public float Hz { get => pulseHz; set => pulseHz = value; }

        public override void _Ready()
        {

            generator = new AudioStreamGenerator();
            generator.BufferLength = 1f;
            this.Stream = generator;
            this.MixTarget = MixTargetEnum.Surround;
        }

        public override void _Process(double delta)
        {
            if (!active) return;
            PitchScale = (1 * curSoundData.PitchLFO.Sample(phase));
            VolumeDb = (10 * curSoundData.VolumeLFO.Sample(phase)) - 5;
            //GD.Print(VolumeDb);
            FillBuffer();
        }

        public void LoadSoundData(SoundData data)
        {
            curSoundData = data;

            //GD.Print(curSoundData.PitchLFO.Sample(1f));
        }

        public void PlayGenerated()
        {
            active = true;
            SetPlayback();
        }

        public void StopGenerated()
        {
            active = false;
            Stop();
        }

        private void SetPlayback()
        {
            sampleHz = generator.MixRate;
            Play();
            playback = (AudioStreamGeneratorPlayback)this.GetStreamPlayback();
            FillBuffer();
        }

        private void FillBuffer()
        {
            if (!bufferPartFinished) return;
            int framesAvailable = playback.GetFramesAvailable();
            if (framesAvailable <= 0) return;
            //GD.Print(framesAvailable);
            float increment = pulseHz / sampleHz;

            bufferPartFinished = false;
            for (int i = 0; i < framesAvailable; i++)
            {
                //GD.Print(phase);
                playback.PushFrame(Vector2.One * curSoundData.Ascelerator.Sample(phase));
                phase = (float)Mathf.PosMod(phase + increment, 1.0);
            }
            bufferPartFinished = true;
        }
    }
}
