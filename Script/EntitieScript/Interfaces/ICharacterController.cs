using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.EntitieScript.Interfaces
{
    public interface ICharacterController
    {
        public bool IsMoving { get; }

        public Godot.Vector2 GetNovementVectorByInput();
        public void Movement(Godot.Vector2 dir, float speed = 0);
    }
}
