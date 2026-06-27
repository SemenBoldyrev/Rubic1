using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Managers
{
    public class SignalBus
    {
        // In case i would need to check signals

        public event Action InventoryOpened;
        public event Action InventoryClosed;

        public void InventoryAcionEmit(bool opened)
        {
            if (opened) InventoryOpened?.Invoke();
            else InventoryClosed?.Invoke();
        }
    }
}
