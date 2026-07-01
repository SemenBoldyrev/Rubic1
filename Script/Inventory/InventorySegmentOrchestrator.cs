using Rubic1.Script.Inventory.Data;
using Rubic1.Script.Inventory.Interfaces;
using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory
{
    public class InventorySegmentOrchestrator : IInventorySegmentsOrchestrator
    {
        private List<InventorySegment> segments = new();
        public List<InventorySegment> Segments => segments;

        public void AddSegment(InventorySegment segment)
        {
            segments.Add(segment);
        }

        public bool CanAddItem(ItemRes item)
        {
            return segments.Where(s => s.CanGiveItem(item)).Any();
        }

        public InventorySegment GetSegmentFromCategory(ItemCategoriesEnum category)
        {
            return segments.Find(s => s.Category == category);
        }

        public bool GiveItem(ItemRes item)
        {
            if (!CanAddItem(item)) return false;
            if (segments.Where(s => s.Category == item.Category).Count() == 0 ) return false;
            return segments.Find(s => s.Category == item.Category).AddItem(item);
        }

        public bool HasItem(ItemRes item)
        {
            return HasItem(item.Id);
        }

        public bool HasItem(int id)
        {
            for (int i = 0; i < segments.Count; i++)
            {
                if (segments[i].ContainItem(id)) return true;
            }
            return false;
        }

        public bool RemoveItem(ItemRes item)
        {
            return RemoveItem(item.Id);
        }

        public bool RemoveItem(int id)
        {
            if (!HasItem(id)) return false;

            InventorySegment relevantSegment = segments.Find(s => s.ContainItem(id));
            return relevantSegment.RemoveItem(id);
        }

        public void RemoveSegment(int segmentId)
        {
            if (segmentId < 0 || segmentId >= segments.Count)
            segments.RemoveAt(segmentId);
        }
    }
}
