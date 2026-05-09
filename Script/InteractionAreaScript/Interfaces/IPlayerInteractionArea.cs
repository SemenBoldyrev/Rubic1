using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.InteractionAreaScript.Interfaces
{
    public interface IPlayerInteractionArea
    {
        public void RegisterNode(AbstractInteractionNode node);
        public void UnregisterNode(AbstractInteractionNode node);
    }
}
