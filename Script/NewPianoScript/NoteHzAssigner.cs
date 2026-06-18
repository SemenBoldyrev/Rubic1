using Godot;
using Rubic1.Script.NewPianoScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript
{
    public class NoteHzAssigner : INoteHzAssigner
    {
        private Dictionary<int, float> HzId = new Dictionary<int, float>()
        {
            {0 ,16.35f},
            {1 ,17.32f}, 
            {2 ,18.35f}, 
            {3 ,19.45f}, 
            {4 ,20.60f}, 
            {5 ,21.83f}, 
            {6 ,23.12f}, 
            {7 ,24.50f}, 
            {8 ,25.96f}, 
            {9 ,27.50f}, 
            {10 ,29.14f}, 
            {11 ,30.87f}
        };

        public float GetNoteHz(int noteId, int octInc = 0)
        {
            int mdl = noteId / HzId.Count;
            float hz = HzId[noteId - mdl];
            return IncreaseOctave(hz ,mdl + octInc);
        }

        public float GetNotePitch(int noteId, int octInc = 0)
        {
            return 1 + (0.049f * (float)(noteId + (HzId.Count * octInc)));
        }

        private float IncreaseOctave(float hz,  int octInc)
        {
            if (octInc <= 0) return hz;
            hz = hz * 2;
            return IncreaseOctave(hz, octInc - 1);
        }
    }
}
