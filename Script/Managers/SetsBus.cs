using Rubic1.Script.Inventory;
using Rubic1.Script.Inventory.Interfaces;
using Rubic1.Script.Inventory.Resources;
using Rubic1.Script.Shared.Sets;
using Rubic1.Script.Shared.Sets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Managers
{
    public static class SetsBus
    {
        private static IItemHolder<ItemRes> itemResHolder = new ItemHolderGDRes<ItemRes>("res://Resources/Items/");

        public static IItemHolder<ItemRes> ItemResHolder { get { return itemResHolder; } }
    }
}
