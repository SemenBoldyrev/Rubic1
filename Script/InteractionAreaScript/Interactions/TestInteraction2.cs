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
            //ManagerBus.DialogManager.DialogEnded += OnDialEnd;
            // ManagerBus.DialogManager.StartDialogByPath("res://Dial/TestDialFolder/SampleExample.json");
            OnDialEnd(true);
            //
            FinishInteractionEmit();
        }

        private void OnDialEnd(bool cor)
        {
            ManagerBus.InventoryManager.RequestAddition(SetsBus.ItemResHolder.GetItemByIndex(2));
            ManagerBus.InventoryManager.RequestAddition(SetsBus.ItemResHolder.GetItemByIndex(1));
            ManagerBus.InventoryManager.RequestAddition(SetsBus.ItemResHolder.GetItemByIndex(3));

            ManagerBus.DialogManager.DialogEnded -= OnDialEnd;
        }
    }
}
