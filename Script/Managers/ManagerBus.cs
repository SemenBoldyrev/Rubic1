using Rubic1.Script.DialogScript;
using Rubic1.Script.DialogScript.Interfaces;
using Rubic1.Script.InteractionAreaScript;
using Rubic1.Script.Sets.AnimationSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Managers
{
    public static class ManagerBus
    {
        private static InteractionManager interactionManager = new();
        private static IDialManager digitalManager = new DialManager();

        public static InteractionManager InteractionManager { get { return interactionManager; } set { interactionManager = value; } }
        public static IDialManager DigitalManager { get {return digitalManager; } set { digitalManager = value; } }
    }
}
