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
        private static InteractionManager interactionManager;

        public static InteractionManager InteractionManager { get { return interactionManager; } set { interactionManager = value; } }
    }
}
