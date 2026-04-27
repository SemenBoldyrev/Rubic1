using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Connectors.Interfaces
{
    public interface ISpriteConnector
    {
        public List<Sprite2D> SpriteList { get; }
        public void FlipX(bool flip = true);
        public void FlipY(bool flip = true);
    }
}
