using Godot;
using Rubic1.Script.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.NewPianoScript.SoundReceiverScript.Listeners.Test
{
    public partial class StaticListenerNoteShowwer: Control
    {
        [Export] Label textLabel;

        public override void _Ready()
        {
            ManagerBus.NoteSubscription.NotePlayed += (note) =>
            {
                textLabel.Text = note.SoundStr;
            };
        }

    }
}
