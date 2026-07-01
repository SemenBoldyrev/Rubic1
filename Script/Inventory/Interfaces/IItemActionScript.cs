using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Interfaces
{
    public interface IItemActionScript
    {
        public bool flag { get; }

        public event Action UsageEnded;

        public void UseItem (Resource respectiveRes = null);
        public void ClearCache();
    }
}
