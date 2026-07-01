using Godot;
using Rubic1.Script.Inventory.Abstract;
using Rubic1.Script.Inventory.Interfaces;
using Rubic1.Script.Inventory.Resources;
using Rubic1.Script.Managers;
using Rubic1.Script.NewPianoScript.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.ActionPresets
{
    public partial class EquipeInstrumentItemAction : ItemActionAbs
    {
        public override bool flag { get {
                if (curInstrument == null) return false;
                return ManagerBus.InventoryManager.InventoryEquipment.ItemIsEquiped(curInstrument);
            } }

        private ItemRes curInstrument = null; 
        private List<string> questions = new List<string>() { "equipe", "return" };

        public override event Action UsageEnded;

        public override void ClearCache()
        {
            ManagerBus.InventoryManager.ItemFeedback.SelectedChoice -= OnChoice;
        }

        public override void UseItem(Resource respectiveRes = null)
        {
            curInstrument = (ItemRes)respectiveRes;
            ManagerBus.InventoryManager.ItemFeedback.SelectedChoice += OnChoice;
            ManagerBus.InventoryManager.ItemFeedback.AskQuestion(questions);
        }

        private  void OnChoice(int choice)
        {
            bool ans;

            switch (choice)
                {
                case 0:
                    ans = ManagerBus.InventoryManager.RequestEquip(curInstrument);
                    break;
                case 1:
                    ManagerBus.InventoryManager.ItemFeedback.GiveInfo(curInstrument.Description);
                    ClearCache();
                    UsageEnded?.Invoke();
                    return;
                default:
                    ans = false;
                    break;
            }

            if (ans)
            {
                ManagerBus.InventoryManager.ItemFeedback.GiveInfo($"You equipped {curInstrument.Name}");
            }
            else
            {
                ManagerBus.InventoryManager.ItemFeedback.GiveInfo($"Unable to equipe {curInstrument.Name}");
            }


            //
            ItemRes curres = ManagerBus.InventoryManager.InventoryEquipment.GetItemByCategory(curInstrument.Category);
            GD.Print($"Item equipped: {ManagerBus.InventoryManager.InventoryEquipment.ItemIsEquiped(curInstrument)}");
            
            if (curres != null)
            {
                GD.Print(curres.Name);
            }
            else
            {
                GD.Print("null");
            }
            //
;
            ClearCache();
            UsageEnded?.Invoke();

        }
    }
}
