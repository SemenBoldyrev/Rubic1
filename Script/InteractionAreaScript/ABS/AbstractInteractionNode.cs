using Godot;
using Rubic1.Script.Connectors;
using Rubic1.Script.InteractionAreaScript.Interfaces;
using Rubic1.Script.Sets.AnimationSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.InteractionAreaScript
{
    public abstract partial class AbstractInteractionNode : Area2D, IInteractionReceiver
    {
        //Add this to a node as separated script with interaction logic
        //then set node to interaction group
        //On interaction start willl start StartInteraction function

        [Export] InteractionAnimations interactionAnimation;
        [Export] Vector2 animationSpan = new Vector2(0,-19);

        private bool interactionPossible = true;

        public bool InteractionPossible { get => interactionPossible; set => interactionPossible = value; }
        public Vector2 Span { get => animationSpan; set => animationSpan = value; }
        public InteractionAnimations InteractionAnimation { get => interactionAnimation; set => interactionAnimation = value; }

        public event Action interactionStarted;
        public event Action interactionFinished;

        public void StartInteractionEmit()
        {
            interactionStarted?.Invoke();
        }

        public void FinishInteractionEmit()
        {
            interactionFinished?.Invoke();
        }

        public abstract void StartInteraction();
    }
}
