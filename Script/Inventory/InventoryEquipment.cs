using Rubic1.Script.Inventory.Data;
using Rubic1.Script.Inventory.Interfaces;
using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Rubic1.Script.Inventory
{
    public class InventoryEquipment : IInventoryEquipment
    {
        private List<EquipmentData> equipmentSlots = new List<EquipmentData>();
        public List<EquipmentData> EquipmentSlots => equipmentSlots;

        public void AddEquipmentSlot(EquipmentData equipmentSlot)
        {
            equipmentSlots.Add(equipmentSlot);
        }

        public bool CanBeEquipped(ItemRes item)
        {
            return GetAvalibleEquipmentSlots(item).Count > 0;
        }

        public bool EquipeItem(ItemRes item)
        {
            if (!CanBeEquipped(item)) return false;
            EquipmentData relevantSlot = GetAvalibleEquipmentSlots(item)[0];
            relevantSlot.RespectiveRes = item;
            return true;
        }

        public ItemRes GetItemByCategory(ItemCategoriesEnum category)
        {
            return equipmentSlots.Find(s => s.Category == category).RespectiveRes;
        }

        public bool ItemIsEquiped(ItemRes item)
        {
            return equipmentSlots.Where(s => s.RespectiveRes == item).Count() > 0;
        }

        public bool ItemIsEquiped(int itemId)
        {
            return equipmentSlots.Where(s => s.RespectiveRes.Id == itemId).Count() > 0;
        }

        public void RemoveEquipmentSlot(int equipmentSlotId)
        {
            if (equipmentSlotId < 0 || equipmentSlotId >= equipmentSlots.Count) return;
            equipmentSlots.RemoveAt(equipmentSlotId);
        }

        public bool UnequipItem(ItemRes item)
        {
            if (!ItemIsEquiped(item)) return false;
            EquipmentData relativeSlot = equipmentSlots.Where(s => s.RespectiveRes == item).ElementAt(0);
            relativeSlot.RespectiveRes = null;
            return true;
        }

        public bool UnequipItem(int itemId)
        {
            List<EquipmentData> relevantSlots = equipmentSlots.Where(s => s.RespectiveRes.Id == itemId).ToList();
            if (relevantSlots.Count == 0) return false;
            EquipmentData relevantSlot = relevantSlots[0];
            relevantSlot.RespectiveRes = null;
            return true;
        }

        public bool UnequipSlot(int slotId)
        {
            if (equipmentSlots[slotId].RespectiveRes == null) return false;
            //bruh
            EquipmentData relevantSlot = equipmentSlots[slotId];
            relevantSlot.RespectiveRes = null;
            return true;
        }

        private List<EquipmentData> GetAvalibleEquipmentSlots(ItemRes item)
        {
            return equipmentSlots.Where(s => s.Category == item.Category && s.RespectiveRes == null).ToList();
        }
    }
}
