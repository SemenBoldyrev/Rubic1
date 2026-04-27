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
    public partial class PlayerSit : PlayerIdle
    {
        [Export] AnimationPlayer animPlayer;

        [Export] Timer TimeToSleep;

        [Export] StateNode SitSleepState;

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

        public override void Exit() 
        {
            TimeToSleep.Timeout -= TimeToSleep_Timeout;
            finished.Invoke();
        }
    }
}
