using Godot;
using HV.Scripts.StateMachine.Example;
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
    public partial class PlayerIdle : StateNode
    {
        [Export] AnimationPlayer animPlayer;
        [Export] CharacterController CharacterController;

        [Export] Timer timeToSit;

        [Export] StateNode MovementState;
        [Export] StateNode LogState;
        [Export] StateNode InstrumentState;
        [Export] StateNode SitState;


        public override event Action<StateNode> transition;
        public override event Action finished;

        public override void Enter()
        {
            animPlayer.Play(PlayerAnimationNames.IDLE);
            timeToSit.Timeout += TimeToSit_Timeout;
            timeToSit.Start();
        }

        private void TimeToSit_Timeout()
        {
            transition.Invoke(SitState);
        }

        public override void Exit()
        {
            timeToSit.Timeout -= TimeToSit_Timeout;
            timeToSit.Stop();
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

            Vector2 dir = CharacterController.GetNovementVectorByInput();
            if (dir == Vector2.Zero)
            {
                return;
            }
            //problems may be with the "events action", they are null on print
            transition.Invoke(MovementState);
        }

        public override void PhysicsUpdate(double delta)
        {
            //-
        }

        public override void Update(double delta)
        {
            //-
        }
    }
}
