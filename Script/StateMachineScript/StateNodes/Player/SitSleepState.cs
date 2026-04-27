using Godot;
using HV.Scripts.StateMachine.Example;
using Rubic1.Script.Sets.AnimationSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.StateMachineScript.StateNodes.Player
{
    public partial class SitSleepState : StateNode
    {
        [Export] AnimationPlayer animPlayer;

        [Export] StateNode IdleState;

        private bool canWakeUp = true;

        public override event Action<StateNode> transition;
        public override event Action finished;

        public override void Enter()
        {
            canWakeUp = true;
            animPlayer.AnimationFinished += AnimPlayer_AnimationFinished;
            animPlayer.Play(PlayerAnimationNames.SIT_SLEEP);
        }

        private void AnimPlayer_AnimationFinished(StringName animName)
        {
            if (animName == PlayerAnimationNames.SIT_WOW)
            {
                animPlayer.AnimationFinished -= AnimPlayer_AnimationFinished;
                transition.Invoke(IdleState);
            }
        }

        public override void Exit()
        {
            finished.Invoke();
        }

        public override void Input(InputEvent @event)
        {
            if (canWakeUp)
            {
                canWakeUp = false;
                animPlayer.Play(PlayerAnimationNames.SIT_WOW);
            }
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
