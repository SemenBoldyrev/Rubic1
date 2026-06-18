using Godot;
using Godot.Collections;
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
    public partial class StupidPianoSoundPlayer : Node, IPianoSoundPlayer
    {
        [Export] int maxAmountSounds = 12;
        [Export] StringName relevantBus;

        //not loving it, need it thou
        private System.Collections.Generic.Dictionary<int, IStupidAudioStreamPlayer> soundPlayerDict = new();
        private InstrumentRes curInstrument;
        private List<IStupidAudioStreamPlayer> curStupidList = new List<IStupidAudioStreamPlayer>();

        private INoteHzAssigner hzAssigner = new NoteHzAssigner();

        private int curBusId;

        public InstrumentRes CurInstrument { get => curInstrument; set => SetNewInstrument(value); }

        public override void _Ready()
        {
            for (int i = 0; i < maxAmountSounds; i++)
            {
                // Here lies selected class
                StupidAudioStreamPlayer player = new StupidAudioStreamPlayer();
                soundPlayerDict.Add(i, player);
                player.Bus = relevantBus;
                this.AddChild(player);
            }
            curBusId = AudioServer.GetBusIndex(relevantBus);
        }

        public void AltSound(bool alt)
        {
            for (int i = 0;i < curStupidList.Count; i++) curStupidList[i].SetAlt(alt);
            GD.Print("alt mode ->", alt);
        }

        public void RequestSound(int soundId)
        {
            if (!soundPlayerDict.Keys.Contains(soundId)) return;
            soundPlayerDict[soundId].StupidPlay();
        }

        public void StopSound(int soundId)
        {
            if (!soundPlayerDict.Keys.Contains(soundId)) return;
            soundPlayerDict[soundId].StupidStop();
        }

        private void SetNewInstrument(InstrumentRes newInstrument)
        {
            curInstrument = newInstrument;

            curStupidList = new();

            foreach (int key in soundPlayerDict.Keys)
            {
                IStupidAudioStreamPlayer player = soundPlayerDict[key];
                if (curInstrument.HzBased)
                {
                    player.LoadData(CurInstrument.SoundData, hz: hzAssigner.GetNoteHz(key, curInstrument.Octave));
                }
                else
                {
                    player.LoadData(CurInstrument.SoundData, pitch: hzAssigner.GetNotePitch(key, curInstrument.Octave));
                }

                curStupidList.Add(player);
            }

            ApplyNewFilters(curBusId, CurInstrument.AudioEffectFilters);
        }

        public void ApplyNewFilters(int busId, Array<AudioEffect> filters)
        {
            int curEffectsCount = AudioServer.GetBusEffectCount(busId);
            for (int i = 0; i < curEffectsCount; i++) AudioServer.RemoveBusEffect(busId, 0);
            int newEffectsCount = filters.Count;
            for (int i = 0; i < newEffectsCount; i++) AudioServer.AddBusEffect(busId, filters[i]);
        }

        public void StopAllSounds()
        {
            for (int i = 0; i< curStupidList.Count; i++)
            {
                curStupidList[i].StupidStop();
            }
        }
    }
}
