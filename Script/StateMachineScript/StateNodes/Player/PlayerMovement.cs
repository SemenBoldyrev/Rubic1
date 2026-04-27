using Godot;
using HV.Scripts.StateMachine.Example;
using Rubic1.Script.Connectors;
using Rubic1.Script.Sets.AnimationSets;
using Rubic1.Script.Sets.KeySets;
using Rubic1.Script.SharedNodesScript;
using Rubic1.Script.SharedNodesScript.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.StateMachineScript.StateNodes.Player
{
    public partial class PlayerMovement : StateNode
    {
        [Export] AnimationPlayer AnimationPlayer;
        [Export] CharacterController CharacterController;

        [Export] StateNode IdleState;
        [Export] StateNode LogState;
        [Export] StateNode InstrumentState;

        [Export] SpriteConnector SpriteBody;
        [Export] int speed = 10;

        public override event Action<StateNode> transition;
        public override event Action finished;

        public override void Enter()
        {
            AnimationPlayer.Play(PlayerAnimationNames.MOVE);
        }

        public override void Exit()
        {
            finished.Invoke();
        }

        public override void Input(InputEvent @event)
        {
            if (@event.IsActionPressed(MainKeyNames.TAKE_INSTRUUMENT))
            {
                transition.Invoke(InstrumentState);
                return;
            }

            if (@event.IsActionPressed(MainKeyNames.OPEN_LOG))
            {
                transition.Invoke(LogState);
                return;
            }
        }

        public override void PhysicsUpdate(double delta)
        {
            //-
        }

        public override void Update(double delta)
        {
            Vector2 dir = CharacterController.GetNovementVectorByInput();
            if (dir.X > 0)
            {
                SpriteBody.FlipX(false);
            }
            else if (dir.X < 0)
            {
                SpriteBody.FlipX();
            }

            if (dir != Vector2.Zero)
            {
                CharacterController.Movement(dir, speed);
            }
            else
            {
                transition.Invoke(IdleState);
            }
        }
    }
}
