using Rubic1.Script.Inventory.Data;
using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Interfaces
{
    public interface IInventoryEquipment
    {
        public List<EquipmentData> EquipmentSlots { get; }

        public void AddEquipmentSlot(EquipmentData equipmentSlot);
        public void RemoveEquipmentSlot(int equipmentSlotId);

        public bool EquipeItem(ItemRes item);
        public bool UnequipItem(ItemRes item);
        public bool UnequipItem(int itemId);
        public bool UnequipSlot(int slotId);

        public bool ItemIsEquiped(ItemRes item);
        public bool ItemIsEquiped(int itemId);

        public bool CanBeEquipped(ItemRes item);

        public ItemRes GetItemByCategory(ItemCategoriesEnum category);

        public bool HasCategory(ItemCategoriesEnum category);
        public bool UnequipeByCategory(ItemCategoriesEnum category);

        public ItemRes GetResFromSlots(ItemCategoriesEnum category);
        public ItemRes GetResFromSlots(int slotId);

    }
}
