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
    public partial class PlayerPlayStance : StateNode
    {
        [Export] AnimationPlayer PlayerAnimationPlayer;

        [Export] StateNode IdleState;

        private bool canPlay = false;

        public override event Action<StateNode> transition;
        public override event Action finished;

        public override void Enter()
        {
            PlayerAnimationPlayer.AnimationFinished += Finish;
            if (canPlay) PlayerAnimationPlayer.Play(PlayerAnimationNames.FLUTE_STAY);
            else PlayerAnimationPlayer.Play(PlayerAnimationNames.GET_ITEM);
        }

        public override void Exit()
        {
            PlayerAnimationPlayer.Play(PlayerAnimationNames.HIDE_ITEM);
        }

        private void Finish(StringName animName)
        {
            if (animName == PlayerAnimationNames.HIDE_ITEM)
            {
                PlayerAnimationPlayer.AnimationFinished -= Finish;
                canPlay = false;
                finished.Invoke();
            }
            if (animName == PlayerAnimationNames.GET_ITEM)
            {
                canPlay = true;
                PlayerAnimationPlayer.Play(PlayerAnimationNames.FLUTE_STAY);
            }
        }

        public override void Input(InputEvent @event)
        {
            if (@event.IsActionPressed(MainKeyNames.TAKE_INSTRUUMENT))
            {
                transition.Invoke(IdleState);
                return;
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
