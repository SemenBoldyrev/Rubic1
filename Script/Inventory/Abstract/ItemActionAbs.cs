using Godot;
using Rubic1.Script.Inventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Abstract
{
    public abstract partial class ItemActionAbs: CSharpScript, IItemActionScript
    {
        public abstract bool flag { get; }

        public abstract event Action UsageEnded;

        public abstract void UseItem(Resource respectiveRes = null);
        public abstract void ClearCache();
    }
}
