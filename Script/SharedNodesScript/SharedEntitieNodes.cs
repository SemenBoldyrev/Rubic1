using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.SharedNodesScript
{
    [GlobalClass]
    public partial class SharedEntitieNodes: Node
    {
        [Export] CharacterController characterController;
        [Export] AnimationPlayer animationPlayer;

        public CharacterController CharacterController => characterController;
        public AnimationPlayer AnimationPlayer => animationPlayer;
    }
}
