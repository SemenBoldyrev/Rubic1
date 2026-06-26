using Rubic1.Script.Inventory.Data;
using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Interfaces
{
    public interface IInventorySegmentsOrchestrator
    {
        //Segments should posess unique categories

        public List<InventorySegment> Segments { get; }

        public bool GiveItem(ItemRes item);
        public bool RemoveItem(ItemRes item);
        public bool RemoveItem(int id);

        public InventorySegment GetSegmentFromCategory(ItemCategoriesEnum category);
        public void AddSegment(InventorySegment segment);
        //do i need this?
        public void RemoveSegment(int segmentId);

        public bool HasItem(ItemRes item);
        public bool HasItem(int id);

        public bool CanAddItem(ItemRes item);
    }
}
