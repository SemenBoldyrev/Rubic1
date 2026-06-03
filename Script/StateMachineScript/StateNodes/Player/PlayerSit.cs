using Godot;
using HV.Scripts.StateMachine.Example;
using Rubic1.Script.Sets.AnimationSets;
using Rubic1.Script.Sets.KeySets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.StateMachineScript.StateNodes.Player
{
    public partial class PlayerSit : StateNode
    {
        [Export] AnimationPlayer animPlayer;
        [Export] CharacterController CharacterController;
        [Export] Timer TimeToSleep;

        [Export] StateNode SitSleepState;

        [Export] StateNode MovementState;
        [Export] StateNode LogState;
        [Export] StateNode InstrumentState;
        [Export] StateNode SitState;

        public override event Action<StateNode> transition;
        public override event Action finished;
        public override void Enter()
        {
            animPlayer.Play(PlayerAnimationNames.SIT);
            TimeToSleep.Timeout += TimeToSleep_Timeout;
            TimeToSleep.Start();
        }

        private void TimeToSleep_Timeout()
        {
            transition.Invoke(SitSleepState);
        }

        //it is not connecting to the base and throws error, so ill just ctrlc ctrlv from PlayerIdle class
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

        public override void Exit() 
        {
            TimeToSleep.Timeout -= TimeToSleep_Timeout;
            finished.Invoke();
        }

        public override void Update(double delta)
        {
            //
        }

        public override void PhysicsUpdate(double delta)
        {
            //
        }
    }
}
