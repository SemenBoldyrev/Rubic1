using Godot;
using Rubic1.Script.InteractionAreaScript.Interfaces;
using Rubic1.Script.Managers;
using Rubic1.Script.Sets.GroupSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.InteractionAreaScript
{
    internal partial class PlayerInteractionNode : Area2D, IPlayerInteractionArea
    {
        public override void _Ready()
        {
            ManagerBus.InteractionManager.PlayerInteractionArea = this;
        }

        public void OnBodyEntered(Area2D body)
        {
            if (body.IsInGroup(GameGroupsNames.INTERACTION_GROUP) && body is AbstractInteractionNode) RegisterNode((AbstractInteractionNode)body);
        }

        public void OnBodyExited(Area2D body)
        {
            if (body.IsInGroup(GameGroupsNames.INTERACTION_GROUP) && body is AbstractInteractionNode) UnregisterNode((AbstractInteractionNode)body);
        }

        public void RegisterNode(AbstractInteractionNode node)
        {
            ManagerBus.InteractionManager.RegisterNode(node);
        }

        public void UnregisterNode(AbstractInteractionNode node)
        {
            ManagerBus.InteractionManager.UnregisterNode(node);
        }
    }
}
