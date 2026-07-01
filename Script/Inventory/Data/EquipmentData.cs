using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Data
{
    public class EquipmentData // <-- for the future generations, as his magesty Gemini said, if the list<struct>[0], then it returns a copy, if list<class>[0], then reference, and at least it helped me...
    {
        private string name;
        private ItemCategoriesEnum category;
        private ItemRes respectiveRes = null;

        public string Name { get { return name; } }
        public ItemCategoriesEnum Category { get { return category; } }
        public ItemRes RespectiveRes { get { return respectiveRes; }  set { respectiveRes = value; } }

        public EquipmentData(string gName, ItemCategoriesEnum gCategory) 
        { 
            this.name = gName;
            this.category = gCategory;
        }
    }
}
