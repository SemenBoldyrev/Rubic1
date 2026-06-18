using Godot;
using Rubic1.Script.NewPianoScript.Data.CustomSoundScript;
using Rubic1.Script.NewPianoScript.Data.SoundDataScript.Enums;
using Rubic1.Script.NewPianoScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Nodes.AudioStreams
{
    public partial class StupidAudioStreamPlayer : AudioStreamPlayer, IStupidAudioStreamPlayer
    {
        //That is one of the stupidest scripts i wrout, stupidli working code btw

        //
        private float sampleHz;
        private float pulseHz = 440.0f; // The frequency of the sound wave.
        private float phase = 0.0f;
        //
        private float basePitch = 1f;

        private AudioStreamGeneratorPlayback playback;
        private AudioStreamGenerator generator;

        private bool active = false;
        private bool ending = false;

        private SoundData curSoundData;

        private float curVolDif;

        private bool bufferPartFinished = true;

        private Timer LFOspeedTimer;
        private Timer LFOspeedTimerPitch;

        private Timer EnvelopAspeedTimer;
        private Timer EnvelopBspeedTimer;
        
        private Timer AltTimer;
        
        private SoundAltTypeEnum nxtAlt = SoundAltTypeEnum.None;
        private SoundAltTypeEnum curAlt = SoundAltTypeEnum.None;

        public SoundAltTypeEnum CurAltTypeEnum { get => curAlt; set => ApplyAlt(value); }

        public override void _Ready()
        {

            generator = new AudioStreamGenerator();
            generator.BufferLength = 1f;

            this.Stream = generator;
            this.MixTarget = MixTargetEnum.Surround;
            this.PitchScale = basePitch;

            SetTimers();
        }

        public override void _Process(double delta)
        {
            if (!active)
            {
                return;
            }

            float nVolume = VolumeDb;
            float nPitch = basePitch;

            if (!EnvelopAspeedTimer.IsStopped())
            {
                nVolume = curSoundData.MinVolume + curSoundData.EnvelopData.EnvelopA.Sample((float)(EnvelopAspeedTimer.WaitTime - EnvelopAspeedTimer.TimeLeft)) * curSoundData.DifVolume;
                curVolDif = Mathf.Abs(nVolume - curSoundData.MinVolume);
            }

            else if (!EnvelopBspeedTimer.IsStopped() && CurAltTypeEnum != SoundAltTypeEnum.Pedal)
            {
                nVolume = curSoundData.MinVolume + curSoundData.EnvelopData.EnvelopB.Sample((float)(EnvelopBspeedTimer.WaitTime - EnvelopBspeedTimer.TimeLeft)) * curVolDif;
            }

            else if (!AltTimer.IsStopped() && CurAltTypeEnum == SoundAltTypeEnum.Pedal)
            {
                nVolume = curSoundData.MinVolume + curSoundData.AltSoundData.PedalEnvelopB.Sample((float)(AltTimer.WaitTime - AltTimer.TimeLeft)) * curVolDif;
            }

            else if (!LFOspeedTimer.IsStopped() && CurAltTypeEnum != SoundAltTypeEnum.Vibrato)
            {
                nVolume = curSoundData.MaxVolume + curSoundData.VolumeLFO.Sample((float)(LFOspeedTimer.WaitTime - LFOspeedTimer.TimeLeft));
            }

            else if (!AltTimer.IsStopped() && CurAltTypeEnum == SoundAltTypeEnum.Vibrato)
            {
                nVolume = curSoundData.MaxVolume + curSoundData.AltSoundData.VibratoLFO.Sample((float)(AltTimer.WaitTime - AltTimer.TimeLeft));
            }

            nPitch = curSoundData.PitchLFO.Sample((float)(LFOspeedTimerPitch.WaitTime - LFOspeedTimerPitch.TimeLeft));

            PitchScale = nPitch;
            VolumeDb = nVolume;

            //GD.Print($"Volume: {VolumeDb}, Pitch: {PitchScale}");
            FillBuffer();
        }

        private void ApplyAlt(SoundAltTypeEnum nAlt)
        {
            //in case i will need something additional
            switch(nAlt)
            {
                case SoundAltTypeEnum.None:
                    if (CurAltTypeEnum == SoundAltTypeEnum.Vibrato) AltTimer.Stop();
                    break;

                case SoundAltTypeEnum.Pedal:
                    AltTimer.WaitTime = curSoundData.AltSoundData.PedalEnvelopB.MaxDomain;
                    AltTimer.OneShot = true;
                    break;

                case SoundAltTypeEnum.Vibrato:
                    AltTimer.WaitTime = curSoundData.AltSoundData.VibratoLFO.MaxDomain;
                    AltTimer.OneShot = false;
                    break;
            }

            if (active)
            {
                nxtAlt = nAlt;
                if (CurAltTypeEnum == SoundAltTypeEnum.Vibrato || nAlt == SoundAltTypeEnum.Vibrato)
                {
                    AltTimer.Start();
                    curAlt = nAlt;
                }
                return;
            }

            curAlt = nAlt;
            nxtAlt = nAlt;
        }

        private void SetTimers(float LFOs = 1f, float LFOPs = 1f, float EAs = 1f, float EBs = 1f)
        {
            if (LFOspeedTimer == null)
            {
                LFOspeedTimer = CreateTimer();
            }
            LFOspeedTimer.WaitTime = LFOs;

            if (LFOspeedTimerPitch == null)
            {
                LFOspeedTimerPitch = CreateTimer();
            }
            LFOspeedTimerPitch.WaitTime = LFOPs;

            if (EnvelopAspeedTimer == null)
            {
                EnvelopAspeedTimer = CreateTimer(true);
                EnvelopAspeedTimer.Timeout += () =>
                {
                    if (curSoundData.PlaybackType == SoundPlaybackEnum.Oneshot || curSoundData.PlaybackType == SoundPlaybackEnum.OneshotHybrid) OneshotStupidStop();
                };
            }
            EnvelopAspeedTimer.WaitTime = EAs;

            if (EnvelopBspeedTimer == null)
            {
                EnvelopBspeedTimer = CreateTimer(true);
                //for envelop
                EnvelopBspeedTimer.Timeout += OnStopEnd;
                //
            }
            EnvelopBspeedTimer.WaitTime = EBs;

            if (AltTimer == null)
            {
                AltTimer = CreateTimer();
                AltTimer.Timeout += OnStopEndAlt;
            }
        }

        private Timer CreateTimer(bool oneshot = false)
        {
            Timer timer = new Timer();
            this.AddChild(timer);
            timer.OneShot = oneshot;
            return timer;
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
                // any print here breaks everything
                playback.PushFrame(Vector2.One * curSoundData.Ascelerator.Sample(phase));
                phase = (float)Mathf.PosMod(phase + increment, 1.0);
            }
            bufferPartFinished = true;
        }

        public void LoadData(SoundData data, float hz = 0, float pitch = 0)
        {
            curSoundData = data;

            if(hz > 0)
            {
                pulseHz = hz;
            }
            if (pitch > 0)
            {
                this.PitchScale = pitch;
                basePitch = pitch;
            }

            CurAltTypeEnum = curSoundData.StartAlt ? curSoundData.AlternativeType : SoundAltTypeEnum.None;

            SetTimers(//!!!
                curSoundData.VolumeLFO.MaxDomain,
                curSoundData.PitchLFO.MaxDomain,
                curSoundData.EnvelopData.EnvelopA.MaxDomain,
                curSoundData.EnvelopData.EnvelopB.MaxDomain
                );
        }

        public void StupidPlay()
        {
            OnStopEnd();
            LFOspeedTimer.Start();
            LFOspeedTimerPitch.Start();
            EnvelopBspeedTimer.Stop();
            if (CurAltTypeEnum == SoundAltTypeEnum.Vibrato) AltTimer.Start();
            else AltTimer.Stop();
            EnvelopAspeedTimer.Start();
            VolumeDb = curSoundData.MinVolume;
            SetPlayback();
            active = true;
            ending = false;
        }


        public void StupidStop()
        {
            if (curSoundData.PlaybackType == SoundPlaybackEnum.Oneshot || ending == true) return;
            EnvelopAspeedTimer.Stop();
            LFOspeedTimer.Stop();
            LFOspeedTimerPitch.Stop();
            if (CurAltTypeEnum == SoundAltTypeEnum.Pedal) AltTimer.Start();
            else EnvelopBspeedTimer.Start();
            ending = true;
        }

        public void OneshotStupidStop()
        {
            // its not binded on key up, bad solution, but it wors
            if (ending == true) return;
            EnvelopAspeedTimer.Stop();
            LFOspeedTimer.Stop();
            LFOspeedTimerPitch.Stop();
            if (CurAltTypeEnum == SoundAltTypeEnum.Pedal) AltTimer.Start();
            else EnvelopBspeedTimer.Start();
            ending = true;
        }

        private void OnStopEnd()
        {
            active = false;
            Stop();
            // so it wont trigger change
            curAlt = nxtAlt;
            LFOspeedTimer.Stop();
            LFOspeedTimerPitch.Stop();
            ending = false;
        }

        private void OnStopEndAlt()
        {
            if (CurAltTypeEnum != SoundAltTypeEnum.Pedal) return;
            active = false;
            Stop();
            // so it wont trigger change
            curAlt = nxtAlt;
            LFOspeedTimer.Stop();
            LFOspeedTimerPitch.Stop();
        }

        public void SetAlt(bool set)
        {
            if (set)
            {
                this.CurAltTypeEnum = curSoundData.AlternativeType;
            }
            else
            {
                this.CurAltTypeEnum = SoundAltTypeEnum.None;
            }
        }
    }
}
