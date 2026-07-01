using Godot;
using Rubic1.Script.Inventory.Data;
using Rubic1.Script.Inventory.Interfaces;
using Rubic1.Script.Inventory.Resources;
using Rubic1.Script.Inventory.UI.Interfaces;
using Rubic1.Script.Managers;
using Rubic1.Script.Shared.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.UI
{
    public partial class InventoryUiBase : Control, IInventoryUiBase, IItemFeedback
    {
        [Export] RichTextLabel infoLable;

        [Export] Container itemRepresentorsContainer;
        [Export] Container choiceContainer;
        [Export] Container segmentContainer;

        [Export] Button exitButton;

        [Export] PackedScene buttonRepresentatorScene; // <-- Should be IInventoryUiButtonRepresentator (I really wish godot could take this things by interfaces)

        [Export] float textSpeed = 0.5f;
        private float ActualTextSpeed => textSpeed / infoLable.Text.Length;

        private InventorySegment curSegment;
        private IInventoryUiItemRepresentor curItemRepresentor;

        private bool CanBeClosed = true;

        public event Action<int> SelectedChoice;

        private bool animateText = false;

        private string lastRead = null;

        public override void _Ready()
        {
            ManagerBus.InventoryManager.ItemFeedback = this;
            UiBus.InventoryUiBase = this;

            exitButton.Pressed += OnExitButtonPress;

            //Show(false);
            ShowItems(new());
            ShowText("");

            Prepare();
        }

        private void OnExitButtonPress()
        {
            Show(false);
        }

        public override void _PhysicsProcess(double delta)  // <-- may be a should use just process
        {
            if (animateText && infoLable.VisibleRatio < 1) infoLable.VisibleRatio += ActualTextSpeed;
            else if (animateText) animateText = false;
        }


        public void Update()
        {
            //need more clearness
            int childAmount = segmentContainer.GetChildCount();
            for (int i = 0; i < childAmount; i++)
            {
                segmentContainer.GetChild(0).QueueFree();
            }

            Prepare();
        }

        public void AskQuestion(List<string> answers, string question = null)
        {
            MakeChoiceButtons(answers);
        }

        public void GiveInfo(string info)
        {
            MakeChoiceButtons();
            ShowText(info);
        }

        public void Show(bool show = true)
        {
            if (!show && !CanBeClosed) return;
            this.Visible = show;
            MakeChoiceButtons();
            if (show)
            {
                ValueButton curBtn = (ValueButton)this.segmentContainer.GetChild(0); //<-- hardcoding!!!
                OpenSegment(curBtn.Value);
            }
            CanBeClosed = true;
            SignalBus.InventoryAcionEmit(show);
            //yea
        }

        private void MakeChoiceButtons(List<string> choices = null)
        {
            ShowText("");
            int choicesAmount = choiceContainer.GetChildCount();

            if (choices != null)
            {
                GD.Print("asked choices: ");
                for (int i = 0;i < choices.Count;i++)
                {
                    GD.Print($"{i}. {choices[i]}");
                }
            }
            else
            {
                GD.Print("Requested choices removal"); // <-- i think this might break, because of id = i and not 0
            }

            for (int i = 0; i < choicesAmount; i++)
            {
                choiceContainer.GetChild(i).QueueFree();
            }

            if (choices == null || choices.Count < 1) return;

            for (int i = 0; i < choices.Count; i++)
            {
                ValueButton btn = new();

                int val = i;
                btn.Value = val;
                btn.ButtonPressedValue += OnChoiceMade;
                btn.Text = choices[i];
                btn.SizeFlagsHorizontal = SizeFlags.ExpandFill;

                choiceContainer.AddChild(btn);
            }

            ValueButton fbtn = (ValueButton)choiceContainer.GetChild(0);
            fbtn.GrabFocus();
        }

        private void OnChoiceMade(int val)
        {
            MakeChoiceButtons(null);
            SelectedChoice?.Invoke(val);
        }

        private void ShowText(string text)
        {
            if (lastRead == text) return;
            lastRead = text;
            infoLable.VisibleRatio = 0f;
            infoLable.Text = text;
            animateText = true;
        }

        private void Prepare()
        {
            List<InventorySegment> segments = ManagerBus.InventoryManager.InventorySegmentsOrchestrator.Segments;
            for (int i = 0; i < segments.Count; i++)
            {
                if (segments[i].Category == ItemCategoriesEnum.Hidden) continue;

                ValueButton btn = new ValueButton(); // <-- hardcoding

                int val = i; // I just scared it may break up badly again
                btn.Text = segments[i].Name;
                btn.AddThemeFontSizeOverride("font_size",32); // <-- hardcoding
                btn.Value = val;

                btn.ButtonPressedValue += OpenSegment;

                segmentContainer.AddChild(btn);
            }

            if (segments.Count > 0) OpenSegment(0);
        }

        private void OpenSegment(int segmentId)
        {
            if (!CanBeClosed) return;
            
            ShowText("");
            curSegment = ManagerBus.InventoryManager.InventorySegmentsOrchestrator.Segments[segmentId];
            UpdateSegment();

            if (itemRepresentorsContainer.GetChildCount() > 0)
            {
                IInventoryUiItemRepresentor curRepr = (IInventoryUiItemRepresentor)itemRepresentorsContainer.GetChild(0);
                curRepr.GrabFocusRepr();
            }
        }

        private void UpdateSegment()
        {
            List<ItemRes> itemRepresentators = curSegment.ItemList;
            ShowItems(itemRepresentators);
        }

        private void ShowItems(List<ItemRes> items) 
        {
            int containerReprAmount = itemRepresentorsContainer.GetChildCount();
            int itemsAmount = items.Count;
            int lastId = 0;

            for (int i = 0; i<containerReprAmount; i++)
            {
                IInventoryUiItemRepresentor repr = (IInventoryUiItemRepresentor)itemRepresentorsContainer.GetChild(i);

                if (i > itemsAmount - 1)
                {
                    repr.LoadData(null);
                }
                else
                {
                    repr.LoadData(items[lastId]);
                    lastId++;
                }

                //repr.Update();
            }

            if (itemsAmount > containerReprAmount)
            {
                for (int i = 0; i<itemsAmount - containerReprAmount ;i++)
                {
                    InventoryUiItemRepresentor repr = buttonRepresentatorScene.Instantiate<InventoryUiItemRepresentor>();

                    repr.LoadData(items[lastId]);
                    repr.Update();

                    repr.InteractionEnded += OnItemInteractionEnded;
                    repr.FocusGrabbed += OnInventoryItemSelected;

                    itemRepresentorsContainer.AddChild(repr);
                    lastId++;
                }
            }
        }

        private void UpdateRepresentators()
        {
            int containerReprAmount = itemRepresentorsContainer.GetChildCount();
            IInventoryUiItemRepresentor repr;

            for (int i = 0;i<containerReprAmount;i++)
            {
                repr = itemRepresentorsContainer.GetChild(i) as IInventoryUiItemRepresentor;
                repr.Update();
            }
        }

        private void OnItemInteractionEnded(ItemRes item)
        {
            CanBeClosed = true;
            // so it will be easier to use
            Button btn = (Button)segmentContainer.GetChild(0);
            UpdateRepresentators();
            btn.GrabFocus();
        }

        private void OnInventoryItemSelected(ItemRes item)
        {
            CanBeClosed = true;
            ShowText(item.Description);
        }
    }
}
