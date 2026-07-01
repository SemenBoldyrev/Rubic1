using Godot;
using Rubic1.Script.Inventory.Resources;
using Rubic1.Script.Inventory.UI.Interfaces;
using Rubic1.Script.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.UI
{
    public partial class InventoryUiItemRepresentor: Control, IInventoryUiItemRepresentor
    {
        [Export] TextureRect Texture;
        [Export] TextureRect FlagTexture;
        [Export] Button ItemActionButton;

        private ItemRes curItem = null;
        private bool interactable = true;

        public event Action<ItemRes> FocusGrabbed;
        public event Action<ItemRes> Clicked;
        public event Action<ItemRes> InteractionEnded;

        public bool Interactable { get => interactable; set => ChangeInteractable(value); }

        public override void _Ready()
        {
            ItemActionButton.Pressed += OnButtonPressed;
            this.FocusEntered += OnFocusGrabed;
            this.ItemActionButton.FocusEntered += OnFocusGrabed;
            Update();
        }

        private void ChangeInteractable(bool activate)
        {
            interactable = activate;

            ItemActionButton.Disabled = !interactable;
            ItemActionButton.FocusMode = interactable ? Control.FocusModeEnum.Accessibility : Control.FocusModeEnum.None;
        }

        private void OnButtonPressed()
        {
            GD.Print($"Action for object representator {curItem}");

            GD.Print($"Item has action script: {curItem.ActionScript != null}");

            if (curItem == null) return;

            if (curItem.ActionScript != null) curItem.ActionScript.UseItem(curItem);
            Clicked?.Invoke(curItem);
        }

        public void LoadData(ItemRes item)
        {
            if (curItem != null && curItem.ActionScript != null) curItem.ActionScript.UsageEnded -= OnInteractionEnd;
            curItem = item;
            Update();
        }

        public void Update()
        {
            if (curItem == null)
            {
                this.Visible = false;
                Texture.Texture = null;
                FlagTexture.Visible = false;
                return;
            }

            this.Visible = true;
            Texture.Texture = curItem.Icon;
            FlagTexture.Visible = curItem.Flag;

            if (curItem.ActionScript != null) curItem.ActionScript.UsageEnded += OnInteractionEnd;
        }

        public void GrabFocusRepr()
        {
            if (!this.Visible) return;
            this.ItemActionButton.GrabFocus();
        }

        private void OnFocusGrabed()
        {
            FocusGrabbed?.Invoke(curItem);
        }

        private void OnInteractionEnd()
        {
            GD.Print("merry cristmas");
            InteractionEnded?.Invoke(curItem);
        }
    }
}
