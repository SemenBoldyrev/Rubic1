using Godot;
using Rubic1.Script.NewPianoScript.Data;
using Rubic1.Script.NewPianoScript.Interfaces;
using Rubic1.Script.NewPianoScript.Nodes.AudioStreams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.PianoSoundPlayers
{
    public partial class PianoSoundPlayer : Node, IPianoSoundPlayer
    {
        [Export] int maxAmountSounds = 12;
        [Export] float basePitch = 1f;
        [Export] float pitchChange = 0.049f;

        private Dictionary<int, AudioStreamPlayer> soundPlayerDict = new();
        private InstrumentRes curInstrument;

        public InstrumentRes CurInstrument { get => curInstrument; set => SetNewInstrument(value); }

        public override void _Ready()
        {
            for (int i = 0; i < maxAmountSounds; i++)
            {
                AudioStreamPlayer player = new();
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
            soundPlayerDict[soundId].Play();
        }

        public void StopSound(int soundId)
        {
            if (!soundPlayerDict.Keys.Contains(soundId)) return;
            soundPlayerDict[soundId].Stop();
        }

        private void SetNewInstrument(InstrumentRes newInstrument)
        {
            curInstrument = newInstrument;

            AudioStream stream = (AudioStream)GD.Load("res://Assets/RedactedSheepSound.mp3");
            foreach (int key in soundPlayerDict.Keys)
            {
                soundPlayerDict[key].Stream = stream;
                soundPlayerDict[key].PitchScale = basePitch + (pitchChange * key);
            }
        }

        public void StopAllSounds()
        {
            throw new NotImplementedException();
        }
    }
}
