using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.UI.Interfaces
{
    public interface IInventoryUiBase
    {
        public void Show(bool show = true);
        public void Update();
    }
}
