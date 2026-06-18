using Rubic1.Script.NewPianoScript.Data.CustomSoundScript;
using Rubic1.Script.NewPianoScript.Data.SoundDataScript.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Interfaces
{
    public interface IStupidAudioStreamPlayer
    {

        public void LoadData(SoundData data, float hz = 0, float pitch = 0);

        public void StupidPlay();
        public void StupidStop();

        public void SetAlt(bool set);
    }
}
