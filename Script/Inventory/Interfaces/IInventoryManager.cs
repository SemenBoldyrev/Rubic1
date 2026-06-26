using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Interfaces
{
    public interface IInventoryManager
    {
        //this system is based on unique categories and single-item-work, fuck multi item work!

        public IInventorySegmentsOrchestrator InventorySegmentsOrchestrator { get; set; }
        public IInventoryEquipment InventoryEquipment { get; set; }
        public IItemFeedback ItemFeedback { get; set; }


        public event Action InventoryChange;

        public event Action<ItemRes> ItemAdded;
        public event Action<ItemRes> ItemRemoved;

        public event Action<ItemRes> ItemEquipped;
        public event Action<ItemRes> ItemUnequipped;


        public bool RequestRemoval(int id);
        public bool RequestRemoval(ItemRes res);
        //public bool RequestRemoval(List<int> idList);
        //public bool RequestRemoval(List<ItemRes> resList);

        public bool RequestAddition(int id);
        public bool RequestAddition(ItemRes res);
        //public bool RequestAddition(List<int> idList);
        //public bool RequestAddition(List<ItemRes> resList);

        public bool RequestEquip(int id);
        public bool RequestEquip(ItemRes res);
        //public bool RequestEquip(List<int> idList);
        //public bool RequestEquip(List<ItemRes> resList);

        public bool RequestUnequip(int id);
        public bool RequestUnequip(ItemRes res);
        public bool RequestUnequipSlot(int id);
        //public bool RequestUnequip(List<int> idList);
        //public bool RequestUnequip(List<ItemRes> resList);

        public bool HasItem(int itemId);
        public bool HasItem(ItemRes item);
        public bool HasItems(List<int> idList);
        public bool HasItems(List<ItemRes> resList);
    }
}
