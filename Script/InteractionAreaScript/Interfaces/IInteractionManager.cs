using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.InteractionAreaScript.Interfaces
{
    public interface IInteractionManager
    {
        public bool CanInteract { get; set; }

        public Node2D PlayerInteractionArea { get; set; } //do i need this this way?

        public event Action InteractionStarted;
        public event Action InteractionEnded;

        public void RegisterNode(AbstractInteractionNode node);

        public void UnregisterNode(AbstractInteractionNode node);

        public void UnregisterAll();
    }
}
