using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.Nodes
{
    [GlobalClass]
    public partial class PianoKey : Button
    {
        [Export] Shortcut shorts;
        [Export] bool Toggalable = false;
        [Export] int KeyValue = 0;

        public event Action<int> PianoKeyPressed;
        public event Action<int> PianoKeyReleased;

        private bool tpressed = false;

        //public override void _Ready()
        //{
        //If i could find a way to bind the shortcuts to HOLD, not just PRESS the button (as done build in)
        //AND via event connection, not input check, then this might be better for optimization
        //But ill stick with it for now
        //}

        public override void _Ready()
        {
            this.ToggleMode = true;
            this.ButtonPressed = false;
            tpressed = false;
        }

        public override void _Input(InputEvent @event)
        {
            //I wont to do presses by mouse or sensor, at least for now
            if (!shorts.MatchesEvent(@event)) return;

            if (Toggalable)
            {
                if (@event.IsPressed() && tpressed == false)
                {
                    tpressed = true;
                    this.ButtonPressed = !this.ButtonPressed;
                    if (this.ButtonPressed) PianoKeyPressed?.Invoke(KeyValue);
                    else PianoKeyReleased?.Invoke(KeyValue);
                }
                else if (@event.IsReleased())
                {
                    tpressed = false;
                }
                    
            }
            else
            {
                if (!this.ButtonPressed && @event.IsPressed())
                {
                    this.ButtonPressed = true;
                    PianoKeyPressed?.Invoke(KeyValue);
                }
                else if (@event.IsReleased())
                {
                    this.ButtonPressed = false;
                    PianoKeyReleased?.Invoke(KeyValue);
                }
            }    
        }

        public void Prepare()
        {
            //in case i would need to make it more stable (!!!)
            this.ButtonPressed = false;
            tpressed = false;
        }
    }
}
