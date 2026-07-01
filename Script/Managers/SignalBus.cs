using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Managers
{
    public static class SignalBus
    {
        // In case i would need to check signals

        public static event Action InventoryOpened;
        public static event Action InventoryClosed;

        public static void InventoryAcionEmit(bool opened)
        {
            if (opened) InventoryOpened?.Invoke();
            else InventoryClosed?.Invoke();
        }
    }
}
