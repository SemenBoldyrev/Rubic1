using Godot;
using Godot.Collections;
using Rubic1.Script.DialogScript.Interfaces;
using Rubic1.Script.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript
{
    public partial class DialManager : Control, IDialManager
    {
        private bool dialogIsPlaying = false;
        private IDialInterface currentDialInterface;
        private IDialCollection currentDialCollection;
        private IDialToParts dialTransferer = new DialToParts();

        private List<IDialInterface> interfacesList = new List<IDialInterface>();

        public bool DialogIsPlaying => dialogIsPlaying;
        public IDialInterface CurrentDialInterface => currentDialInterface;
        public IDialCollection CurrentDialCollection => currentDialCollection;

        public event Action<bool> DialogEnded;

        // make so interfaces are child nodes loadeed on start

        public override void _Ready()
        {
            Array<Node> tmpLst = this.GetChildren();
            foreach (Node node in tmpLst)
            {
                interfacesList.Add((IDialInterface)node);
            }

            ManagerBus.DialogManager = this;
        }

        public void ForseEndDialog()
        {
            if (DialogIsPlaying && CurrentDialInterface != null)
            {
                EndDialLoop(true);
            }
        }

        public void StartDialog(IDialCollection dialCollection, int dialInterface = 0)
        {
            GD.Print($"Dialog Started: \n - first part -> {dialCollection.GetDialPart(0).Dial}, \n - interface -> {dialInterface}");
            if (dialogIsPlaying) return;
            dialogIsPlaying = true;
            currentDialInterface = interfacesList[dialInterface];

            StartDialLoop(dialCollection, currentDialInterface);
        }

        private void StartDialLoop(IDialCollection dial, IDialInterface inter)
        {
            currentDialCollection = dial;
            // i dont want to just clear ALL connections, so i made dedicated function, not event like (id,even) => {}
            inter.PartEnded += OnNextDialPart;
            ShowDialPart(dial, 0, inter);
        }

        private void OnNextDialPart(int id, int even)
        {
            // maybe ill remove "id == 0" in future
            if (even == 4 || id == 0)
            {
                EndDialLoop();
                return;
            }
            try
            {
                ShowDialPart(CurrentDialCollection, id, CurrentDialInterface);
            }
            catch (Exception ex)
            {
                GD.Print($"something went wrong with dialog: -- {ex.Message}");
                OnNextDialPart(0, 4);
            }
        }

        private void EndDialLoop(bool force = false)
        {
            currentDialInterface.PartEnded -= OnNextDialPart;
            CurrentDialInterface.MakeVisible(false);

            currentDialCollection = null;
            currentDialInterface = null;

            dialogIsPlaying = false;
            DialogEnded?.Invoke(!force);
        }

        private void ShowDialPart(IDialCollection dial, int id, IDialInterface inter)
        {
            inter.ClearDial();
            inter.ShowDial(dial.GetDialPart(id));
        }

        public void StartDialogByPath(string path, int dialInterface = 0)
        {
            IDialCollection collection = new DialCollection();
            collection.LoadFromList(dialTransferer.FileToList(path));
            StartDialog(collection, dialInterface);
        }
    }
}
