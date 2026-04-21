using HV.Scripts.StateMachine.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.StateMachineScript.Interfaces
{
    internal interface IStateMachine
    {
        public string CurStateName { get; }
        public StateNode CurState { get; }
        public Dictionary<string, StateNode> PlayerStates { get; }
        public StateNode InitialState { get; set; }
        public event Action<StateNode> StateChanged;
    }
}
