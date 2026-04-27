 using Godot;
using Rubic1.Script.StateMachineScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HV.Scripts.StateMachine.Example
{
    [GlobalClass]
    public partial class StateMachine: Node, IStateMachine
    {

        private Dictionary<string, StateNode> playerStates = new();
        [Export] private StateNode initialState;
        private StateNode nextState;
        private StateNode curState;

        public event Action<StateNode> StateChanged;

        public string CurStateName => curState.Name;
        public StateNode CurState => curState;

        public Dictionary<string, StateNode> PlayerStates => playerStates;

        public StateNode InitialState { get => initialState; set => initialState = value; }


        //INIT
        public override void _Ready()
        {
            SetStates();
            curState = initialState;
            curState.Enter();
        }

        //CONTROLL
        public override void _Input(InputEvent @event)
        {
            //not everyone need this
            if (curState != null) { curState.Input(@event); }
        }

        public override void _Process(double delta)
        {
            if (curState != null) { curState.Update(delta); }
        }

        public override void _PhysicsProcess(double delta)
        {
            if (curState != null) { curState.PhysicsUpdate(delta); }
        }

        //DEFINE
        private void SetStates()
        {
            foreach (StateNode state in this.GetChildren())
            {
                if (state is StateNode)
                {
                    playerStates.Add(state.Name.ToString().ToLower(), state);
                    state.transition += StateTransitionExit;
                    state.finished += StateTransitionFinish;
                }
            }
        }

        private void StateTransitionExit(StateNode newState)
        {
            try
            {
                if (curState == newState) { return; } //? need this ?

                nextState = newState;
                curState.Exit();
            }
            catch
            {
                GD.Print("State transition problem!");
                curState = initialState;
                curState.Enter();
            }
        }

        private void StateTransitionFinish()
        {
            curState = nextState;
            nextState = null;
            curState.Enter();

            StateChanged?.Invoke(curState);
        }
    }
}
