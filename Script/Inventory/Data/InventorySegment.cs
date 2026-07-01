using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Data
{
    public struct InventorySegment
    {
        private string name;
        private int limit;
        private Dictionary<int, ItemRes> itemDict = new();
        private ItemCategoriesEnum category;


        public string Name { get => name; }
        public int Limit { get => limit > -1 ? limit : 999; }
        public  List<ItemRes> ItemList {  get => itemDict.Values.ToList(); }
        public ItemCategoriesEnum Category { get => category; }


        public InventorySegment(string name, int limit, ItemCategoriesEnum category)
        {
            this.name = name;
            this.limit = limit;
            this.category = category;
        }


        public bool AddItem(ItemRes item)
        {
            if(!CheckItemAccesability(item)) return false;

            itemDict.Add(item.Id, item);
            return true;
        }

        public bool RemoveItem(int itemId)
        {
            if(!itemDict.ContainsKey(itemId)) return false;

            itemDict.Remove(itemId);
            return true;
        }

        public bool ContainItem(ItemRes item)
        {
            return ItemList.Contains(item);
        }

        public bool ContainItem(int itemId)
        {
            return ItemList.Where(i => i.Id == itemId).Any();
        }


        public bool CanGiveItem(ItemRes item)
        {
            return CheckItemAccesability(item);
        }

        public bool CanGiveItem(List<ItemRes> items)
        {
            if ((items.Count + itemDict.Count) > Limit) return false;
            List<int> nidl = new List<int>();
            for(int i = 0; i < items.Count; i++)
            {
                if (!CheckItemAccesability(items[i])) return false;
                if (nidl.Contains(items[i].Id)) return false;
                nidl.Add(items[i].Id);
            }
            return true;
        }


        private bool CheckItemAccesability(ItemRes item)
        {
            return !(itemDict.ContainsKey(item.Id) ||
                item.Category != category ||
                itemDict.Count + 1 >= Limit);
        }
    }
}
