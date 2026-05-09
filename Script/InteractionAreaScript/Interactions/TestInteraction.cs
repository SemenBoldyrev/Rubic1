using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.InteractionAreaScript.Interactions
{
    public partial class TestInteraction : AbstractInteractionNode
    {
        public override void StartInteraction()
        {
            StartInteractionEmit();
            GD.Print("Interaction successful");
            FinishInteractionEmit();
        }
    }
}
