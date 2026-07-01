using Rubic1.Script.Inventory.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Managers
{
    public static class UiBus
    {
        private static IInventoryUiBase inventoryUiBase;

        public static IInventoryUiBase InventoryUiBase {  get { return inventoryUiBase; }  set { inventoryUiBase = value; } }
    }
}
