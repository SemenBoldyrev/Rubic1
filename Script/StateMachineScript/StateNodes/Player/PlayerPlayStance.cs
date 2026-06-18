using Godot;
using HV.Scripts.StateMachine.Example;
using Rubic1.Script.Managers;
using Rubic1.Script.NewPianoScript.Data;
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

        [Export] Timer DanceAnimSpanTimer;

        private bool canPlay = false;

        public override event Action<StateNode> transition;
        public override event Action finished;

        private bool finishing = false;

        private InstrumentRes instrument = GD.Load<InstrumentRes>("res://Resources/Instruments/TestInstrument.tres");

        public override void Enter()
        {
            PlayerAnimationPlayer.AnimationFinished += Finish;
            // what?
            if (canPlay) PlayerAnimationPlayer.Play(PlayerAnimationNames.FLUTE_STAY);
            else PlayerAnimationPlayer.Play(PlayerAnimationNames.GET_ITEM);
            finishing = false;
            DanceAnimSpanTimer.Timeout += StopDancing;
        }

        private void StopDancing()
        {
            //in case [finishing]
            if (ManagerBus.PianoManager.PianoKeyboard.AnyKeyPressed || finishing) return;
            PlayerAnimationPlayer.Play(PlayerAnimationNames.FLUTE_STAY);
        }

        public override void Exit()
        {
            canPlay = false;
            DanceAnimSpanTimer.Stop();
            PlayerAnimationPlayer.Play(PlayerAnimationNames.HIDE_ITEM);
        }

        private void Finish(StringName animName)
        {
            if (animName == PlayerAnimationNames.HIDE_ITEM)
            {
                canPlay = false;
                // somehow needs to be here, or already-connected error
                PlayerAnimationPlayer.AnimationFinished -= Finish;
                DanceAnimSpanTimer.Timeout -= StopDancing;
                //
                finished.Invoke();
            }
            if (animName == PlayerAnimationNames.GET_ITEM)
            {
                PlayerAnimationPlayer.Play(PlayerAnimationNames.FLUTE_STAY);
                ManagerBus.PianoManager.StartPianoSession(instrument);
                canPlay = true;
            }
        }

        public override void Input(InputEvent @event)
        {
            if (!canPlay) return;
            if (@event.IsActionPressed(MainKeyNames.TAKE_INSTRUUMENT))
            {
                finishing = true;
                ManagerBus.PianoManager.StopPianoSession();
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
            if (!canPlay) return;
            bool pianoIsPlaying = ManagerBus.PianoManager.PianoKeyboard.AnyKeyPressed;

            if (pianoIsPlaying)
            {
                if (PlayerAnimationPlayer.CurrentAnimation != PlayerAnimationNames.FLUTE_PLAY) PlayerAnimationPlayer.Play(PlayerAnimationNames.FLUTE_PLAY);
            }
            else if (DanceAnimSpanTimer.IsStopped())
            {
                DanceAnimSpanTimer.Start();
            }
            
        }
    }
}
