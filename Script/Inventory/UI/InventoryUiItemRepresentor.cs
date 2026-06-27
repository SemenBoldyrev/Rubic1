using Godot;
using Rubic1.Script.Inventory.Resources;
using Rubic1.Script.Inventory.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.UI
{
    public partial class InventoryUiItemRepresentor: Node, IInventoryUiItemRepresentor
    {
        [Export] TextureRect Texture;
        [Export] TextureRect FlagTexture;
        [Export] Button ItemActionButton;

        private ItemRes curItem = null;
        private bool interactable = true;

        public bool Interactable { get => interactable; set => ChangeInteractable(value); }

        public override void _Ready()
        {
            ItemActionButton.Pressed += OnButtonPressed;
        }

        private void ChangeInteractable(bool activate)
        {
            interactable = activate;

            ItemActionButton.Disabled = !interactable;
            ItemActionButton.FocusMode = interactable ? Control.FocusModeEnum.Accessibility : Control.FocusModeEnum.None;
        }

        private void OnButtonPressed()
        {
            if (curItem != null) return;

            curItem.Script.UseItem();
        }

        public void LoadData(ItemRes item)
        {
            curItem = item;
        }

        public void Update()
        {
            if (curItem == null)
            {
                Texture.Texture = null;
                FlagTexture.Texture = null;
                return;
            }

            Texture.Texture = curItem.Icon;
            FlagTexture.Visible = curItem.Flag;
        }
    }
}
