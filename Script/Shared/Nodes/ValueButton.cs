using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Shared.Nodes
{
    [GlobalClass]
    public partial class ValueButton: Button
    {
        // mainly int, atleast for now

        [Export] int val = 0;
        public int Value { get { return val; } set { val = value; } }

        public event Action<int> ButtonPressedValue;

        public override void _Ready()
        {
            this.ButtonUp += () => { this.ButtonPressedValue?.Invoke(val); };
        }
    }
}
