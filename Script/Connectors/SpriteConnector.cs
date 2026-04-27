using Godot;
using Rubic1.Script.Connectors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Connectors
{
    [GlobalClass]
    public partial class SpriteConnector : Node2D, ISpriteConnector
    {
        private List<Sprite2D> spriteList = new List<Sprite2D>();
        public List<Sprite2D> SpriteList => spriteList;

        public override void _Ready()
        {
            //maybe need to make more secure
            foreach (Sprite2D sprite in this.GetChildren())
            {
                spriteList.Add(sprite);
            }
        }

        public void FlipX(bool flip = true)
        {
            for (int i = 0; i < spriteList.Count; i++)
            {
                spriteList[i].FlipH = flip;
            }
        }

        public void FlipY(bool flip = true)
        {
            for (int i = 0; i < spriteList.Count; i++)
            {
                spriteList[i].FlipV = flip;
            }
        }
    }
}
