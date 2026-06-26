using Rubic1.Script.Inventory.Data;
using Rubic1.Script.Inventory.Interfaces;
using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory
{
    public class InventoryManager : IInventoryManager
    {
        //its like 1:41 on clock, this will probably break somwhere idk, i havent tested it, so "godspeed" i think?

        private List<InventorySegment> inventorySegmentsList = new List<InventorySegment>()
        { 
            new InventorySegment("Instruments", -1, ItemCategoriesEnum.Instrument),
            new InventorySegment("Keys", -1, ItemCategoriesEnum.Key),
            new InventorySegment("Hidden", -1, ItemCategoriesEnum.Hidden)  };
        private List<EquipmentData> equipmentDataList = new List<EquipmentData>()
        { 
            new EquipmentData("Instrument", ItemCategoriesEnum.Instrument)  };


        private IInventorySegmentsOrchestrator inventorySegmentsOrchestrator = new InventorySegmentOrchestrator();
        private IInventoryEquipment inventoryEquipment = new InventoryEquipment();
        //should be assigned by relevant ui element (may be there is a better solution)
        private IItemFeedback itemFeedback = null;

        public IInventorySegmentsOrchestrator InventorySegmentsOrchestrator { get => inventorySegmentsOrchestrator; set => inventorySegmentsOrchestrator = value; }
        public IInventoryEquipment InventoryEquipment { get => inventoryEquipment; set => inventoryEquipment = value; }
        public IItemFeedback ItemFeedback { get => itemFeedback; set => itemFeedback = value; }

        public event Action InventoryChange;
        public event Action<ItemRes> ItemAdded;
        public event Action<ItemRes> ItemRemoved;
        public event Action<ItemRes> ItemEquipped;
        public event Action<ItemRes> ItemUnequipped;


        public InventoryManager()
        {
            for (int i = 0; i < inventorySegmentsList.Count; i++) this.InventorySegmentsOrchestrator.AddSegment(inventorySegmentsList[i]);
            for (int i = 0; i < equipmentDataList.Count; i++) this.InventoryEquipment.AddEquipmentSlot(equipmentDataList[i]);
        }


        public bool HasItem(int itemId)
        {
            return this.InventorySegmentsOrchestrator.HasItem(itemId) || this.InventoryEquipment.ItemIsEquiped(itemId);
        }

        public bool HasItem(ItemRes item)
        {
            return HasItem(item.Id);
        }

        public bool HasItems(List<int> idList)
        {
            for (int i = 0; i < idList.Count; i++)
            {
                if (!HasItem(idList[i])) return false;
            }
            return true;
        }

        public bool HasItems(List<ItemRes> resList)
        {
            return HasItems(resList.Select(i => i.Id).ToList());
        }

        public bool RequestAddition(int id)
        {
            // !!! NEED ITEM DATABASE !!!
            throw new NotImplementedException();
        }

        public bool RequestAddition(ItemRes res)
        {
            if (!this.inventorySegmentsOrchestrator.CanAddItem(res)) return false;
            bool ans = this.inventorySegmentsOrchestrator.CanAddItem(res);

            if (ans)
            {
                ItemAdded?.Invoke(res);
                InventoryChange?.Invoke();
            }
            return ans;
        }

        public bool RequestEquip(int id)
        {
            // !!! NEED ITEM DATABASE !!!
            throw new NotImplementedException();
        }

        public bool RequestEquip(ItemRes res)
        {
            if (!this.InventoryEquipment.CanBeEquipped(res)) return false;
            bool ans = this.inventoryEquipment.EquipeItem(res);

            if (ans)
            {
                ItemEquipped?.Invoke(res);
                InventoryChange?.Invoke();
            }
            return ans;
        }

        public bool RequestRemoval(int id)
        {
            bool ans = this.inventorySegmentsOrchestrator.RemoveItem(id);

            if (ans)
            {
                //maybe i can just use pure resources from the folder?
                //or not sending a signal on unequip?
                //or maybe even return not boolean but resource itself, so i wont be seeking it in database?

                // !!! NEED ITEM DATABASE !!!
                //considering the items are static
                ItemRemoved?.Invoke(new ItemRes());
                InventoryChange?.Invoke();
            }
            return ans;
        }

        public bool RequestRemoval(ItemRes res)
        {
            bool ans = this.inventorySegmentsOrchestrator.RemoveItem(res);

            if (ans)
            {
                ItemRemoved?.Invoke(res);
                InventoryChange?.Invoke();
            }
            return ans;
        }

        public bool RequestUnequip(int id)
        {
            bool ans = this.InventoryEquipment.UnequipItem(id);

            if (ans)
            {
                // !!! NEED ITEM DATABASE !!!
                //considering the items are static
                ItemUnequipped?.Invoke(new ItemRes());
                InventoryChange?.Invoke();
            }
            return ans;
        }

        public bool RequestUnequip(ItemRes res)
        {
            bool ans = this.InventoryEquipment.UnequipItem(res);

            if (ans)
            {
                ItemUnequipped?.Invoke(res);
                InventoryChange?.Invoke();
            }
            return ans;
        }

        public bool RequestUnequipSlot(int id)
        {
            bool ans = this.InventoryEquipment.UnequipSlot(id);

            if (ans)
            {
                // !!! NEED ITEM DATABASE !!!
                //considering the items are static
                ItemUnequipped?.Invoke(new ItemRes());
                InventoryChange?.Invoke();
            }
            return ans;
        }
    }
}
