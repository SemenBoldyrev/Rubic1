using Godot;
using Rubic1.Script.DialogScript;
using Rubic1.Script.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.InteractionAreaScript.Interactions
{
    public partial class TestInteraction2 : AbstractInteractionNode
    {
        public override void StartInteraction()
        {
            StartInteractionEmit();
            GD.Print("Interaction successful 2");
            //
            ManagerBus.DialogManager.StartDialogByPath("res://Dial/TestDialFolder/SampleExample.json");
            //
            FinishInteractionEmit();
        }
    }
}
