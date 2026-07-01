using Rubic1.Script.Inventory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.UI.Interfaces
{
    public interface IInventoryUiItemRepresentor
    {
        public bool Interactable { get; set; }

        public event Action<ItemRes> FocusGrabbed;
        public event Action<ItemRes> Clicked;
        public event Action<ItemRes> InteractionEnded;

        public void LoadData(ItemRes item);
        public void Update();
        public void GrabFocusRepr();
    }
}
