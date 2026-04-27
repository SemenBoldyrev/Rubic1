using Godot;
using HV.Scripts.StateMachine.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.SharedNodesScript.States
{
    public partial class PlayerStateNodes: Node
    {
        [Export] StateNode Idle;
        [Export] StateNode Movement;

        public StateNode IDLE => Idle;
        public StateNode MOVEMENT => Movement;
    }
}
