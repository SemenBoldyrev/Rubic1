using Godot;
using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Shared.Sets.Interfaces
{
    public interface IItemHolder<T>
    {
        public T GetItemByIndex(int key);
    }
}
