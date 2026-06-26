using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Data
{
    public struct EquipmentData
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
