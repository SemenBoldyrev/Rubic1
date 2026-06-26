using Rubic1.Script.DialogScript;
using Rubic1.Script.DialogScript.Interfaces;
using Rubic1.Script.InteractionAreaScript;
using Rubic1.Script.Sets.AnimationSets;
using Rubic1.Script.NewPianoScript;
using Rubic1.Script.NewPianoScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubic1.Script.Inventory.Interfaces;
using Rubic1.Script.Inventory;

namespace Rubic1.Script.Managers
{
    public static class ManagerBus
    {
        private static InteractionManager interactionManager = new();
        private static IDialManager dialogManager = new DialManager();
        private static IPianoManager pianoManager = new PianoManager();
        private static INoteSubscription noteSubscription = new NoteSubscription();
        private static IInventoryManager inventoryManager = new InventoryManager();

        public static InteractionManager InteractionManager { get { return interactionManager; } set { interactionManager = value; } }
        public static IDialManager DialogManager { get {return dialogManager; } set { dialogManager = value; } }
        public static IPianoManager PianoManager { get {return pianoManager; } set { pianoManager = value; } }
        public static INoteSubscription NoteSubscription { get { return noteSubscription; } set { noteSubscription = value; } }
        public static IInventoryManager InventoryManager { get { return inventoryManager; } set { inventoryManager = value; } }
    }
}
