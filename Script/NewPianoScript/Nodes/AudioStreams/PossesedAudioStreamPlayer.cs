using Godot;
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
    public partial class PossesedAudioStreamPlayer: AudioStreamPlayer
    {
        //
        private AudioStreamGeneratorPlayback _playback; // Will hold the AudioStreamGeneratorPlayback.
        private float _sampleHz;
        private float _pulseHz = 440.0f; // The frequency of the sound wave.
        private double phase = 0.0;
        private AudioStreamGenerator generator;
        //

        private bool active = false;

        private SoundData curSoundData;
        public override void _Ready()
        {

            generator = new AudioStreamGenerator();
            this.Stream = generator;
        }

        public override void _Process(double delta)
        {
            if (!active) return;
            FillBuffer();
        }

        public void LoadSoundData(SoundData data)
        {
            curSoundData = data;
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
            _sampleHz = generator.MixRate;
            Play();
            _playback = (AudioStreamGeneratorPlayback)this.GetStreamPlayback();
            FillBuffer();
        }

        private void FillBuffer()
        {
            float increment = _pulseHz / _sampleHz;
            int framesAvailable = _playback.GetFramesAvailable();

            for (int i = 0; i < framesAvailable; i++)
            {
                _playback.PushFrame(Vector2.One * (float)Mathf.Sin(phase * Mathf.Tau));
                phase = Mathf.PosMod(phase + increment, 1.0);
            }
        }
    }
}
