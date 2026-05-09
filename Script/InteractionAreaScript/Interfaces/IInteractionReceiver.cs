using Godot;
using Rubic1.Script.Sets.AnimationSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.InteractionAreaScript.Interfaces
{
    public interface IInteractionReceiver
    {
        public bool InteractionPossible { get; set; }
        public Vector2 Span { get; set; }
        public InteractionAnimations InteractionAnimation {  get; set; }

        public event Action interactionStarted;
        public event Action interactionFinished;

        public void StartInteraction();
    }
}
