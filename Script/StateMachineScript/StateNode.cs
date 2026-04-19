using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HV.Scripts.StateMachine.Example
{
	public abstract partial class StateNode: Node
	{
		//transition -> to make "Exit()"
		//finish -> to change state
		//use transition, then finish, so it could do exit things and only them change state, good in giving controll after exit animation
		public abstract event EventHandler<string> transition;
		public abstract event Action finished;
		//---
		public abstract int MoveSpeed { get; }
		public abstract void Enter();
		public abstract void Exit();
		public abstract void Input(InputEvent @event);
		public abstract void Update(double delta);
		public abstract void PhysicsUpdate(double delta);
	}
}
