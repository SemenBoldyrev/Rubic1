using Godot;
using Rubic1.Script.Connectors;
using Rubic1.Script.InteractionAreaScript.Interfaces;
using Rubic1.Script.Managers;
using Rubic1.Script.Sets.KeySets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.InteractionAreaScript
{
    public partial class InteractionManager : Node, IInteractionManager
    {
        [Export] AnimationPlayer animPlayer;
        [Export] SpriteConnector spriteBody;

        private bool canInteract = true;
        private List<AbstractInteractionNode> interactionNodes = new List<AbstractInteractionNode>();

        private Node MainInteractionArea;

        private AbstractInteractionNode curInteraction;

        private Node2D player;

        public event Action InteractionStarted;
        public event Action InteractionEnded;

        public bool CanInteract { get => canInteract; set => canInteract = value; }
        public Node2D PlayerInteractionArea { get => player; set => player = value; }

        public override void _Ready()
        { 
            canInteract = true;
            this.ProcessMode = ProcessModeEnum.Disabled;
            ManagerBus.InteractionManager = this;
        }

        public override void _Process(double delta)
        {
            if (canInteract && player != null)
            {
                FocusInteraction(GetClosestNode(player, interactionNodes));
                return;
            }
            FocusInteraction();
        }

        public override void _Input(InputEvent @event)
        {
            if (canInteract && @event.IsActionPressed(MainKeyNames.ACTION) && curInteraction != null)
            {
                curInteraction.interactionFinished += EndInteraction;
                StartInteraction();
            }
        }


        private AbstractInteractionNode GetClosestNode(Node2D target, List<AbstractInteractionNode> nodes)
        {
            if(nodes.Count < 1) return null;

            float curDist = target.GlobalPosition.DistanceTo(nodes[0].GlobalPosition); ;
            float nxtDist = 0f;

            AbstractInteractionNode ans = nodes[0];
            for (int i = 0; i < nodes.Count; i++)
            {
                nxtDist = target.GlobalPosition.DistanceTo(nodes[i].GlobalPosition);
                if (nxtDist < curDist)
                {
                    curDist = nxtDist;
                    ans = nodes[i];
                }
            }
            return ans;
        }

        private void FocusInteraction(AbstractInteractionNode node = null)
        {
            if (node == null)
            {
                curInteraction = null;
                spriteBody.Visible = false;
                animPlayer.Stop();
                return;
            }
            if (curInteraction == node) return;
            curInteraction = node;
            spriteBody.Position = node.GlobalPosition + node.Span;
            animPlayer.Play(node.InteractionAnimation.ToString());
            spriteBody.Visible = true;
            curInteraction = node;
        }

        private void StartInteraction()
        {
            CanInteract = false;
            InteractionStarted?.Invoke();
            curInteraction.StartInteraction();
        }

        private void EndInteraction()
        {
            curInteraction.interactionFinished += EndInteraction; //for safe 
            CanInteract = true;
            InteractionEnded?.Invoke();
        }

        public void RegisterNode(AbstractInteractionNode node)
        {
            interactionNodes.Add(node);
            ProcessMode = ProcessModeEnum.Pausable;
        }

        public void UnregisterNode(AbstractInteractionNode node)
        {
            if (interactionNodes.Contains(node))
            {
                interactionNodes.Remove(node);
            }
            if (interactionNodes.Count < 1)
            {
                ProcessMode = ProcessModeEnum.Disabled;
                FocusInteraction();
            }
        }

        public void UnregisterAll()
        {
            for (int i = 0; i < interactionNodes.Count; i++) UnregisterNode(interactionNodes[i]);
        }
    }
}
