using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Interfaces
{
    public interface INoteHzAssigner
    {
        public float GetNoteHz(int noteId, int octInc = 0);
        public float GetNotePitch(int noteId, int octInc = 0);

    }
}
