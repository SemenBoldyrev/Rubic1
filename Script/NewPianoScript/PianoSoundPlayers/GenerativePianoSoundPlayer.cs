using Godot;
using Rubic1.Script.NewPianoScript.Data;
using Rubic1.Script.NewPianoScript.Interfaces;
using Rubic1.Script.NewPianoScript.Nodes.AudioStreams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.PianoSoundPlayers
{
    public partial class GenerativePianoSoundPlayer : Node, IPianoSoundPlayer
    {
        [Export] int maxAmountSounds = 12;
        [Export] float baseHz = 261.63f;
        [Export] float pitchChange = 0.049f;
        [Export] int quality = 44100;

        private Dictionary<int, GenerativeAudioStreamPlayer> soundPlayerDict = new();
        private InstrumentRes curInstrument;

        private INoteHzAssigner hzAssigner = new NoteHzAssigner();

        public InstrumentRes CurInstrument { get => curInstrument; set => SetNewInstrument(value); }

        public override void _Ready()
        {
            for (int i = 0; i < maxAmountSounds; i++)
            {
                GenerativeAudioStreamPlayer player = new();
                soundPlayerDict.Add(i, player);
                // !!!HARDSTRING!!!
                player.Bus = "PianoSound";
                this.AddChild(player);
            }
        }

        public void AltSound(bool alt)
        {
            GD.Print("alt mode ->", alt);
        }

        public void RequestSound(int soundId)
        {
            if (!soundPlayerDict.Keys.Contains(soundId)) return;
            soundPlayerDict[soundId].PlayGenerated();
        }

        public void StopSound(int soundId)
        {
            if (!soundPlayerDict.Keys.Contains(soundId)) return;
            soundPlayerDict[soundId].StopGenerated();
        }

        private void SetNewInstrument(InstrumentRes newInstrument)
        {
            curInstrument = newInstrument;

            //AudioStream stream = (AudioStream)GD.Load("res://Assets/RedactedSheepSound.mp3");
            foreach (int key in soundPlayerDict.Keys)
            {
                GenerativeAudioStreamPlayer player = soundPlayerDict[key];

                player.Hz = hzAssigner.GetNoteHz(key, curInstrument.Octave);
                player.LoadSoundData(new Data.CustomSoundScript.SoundData());
                //soundPlayerDict[key].PitchScale = basePitch + (pitchChange * key);
                player.LoadSoundData(CurInstrument.SoundData);
            }
        }

        public void StopAllSounds()
        {
            throw new NotImplementedException();
        }
    }
}

