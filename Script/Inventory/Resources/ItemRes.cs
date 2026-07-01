using Godot;
using Rubic1.Script.Inventory.Abstract;
using Rubic1.Script.Inventory.Data;
using Rubic1.Script.Inventory.Interfaces;
using Rubic1.Script.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Resources
{
    [GlobalClass]
    public partial class ItemRes : Resource, IHaveIndex
    {
        //in case i would need it
        [Export] private int id;

        [Export] private CompressedTexture2D icon;
        [Export] private string name;
        [Export] private string description;
        [Export] private bool flag;

        [Export] private ItemCategoriesEnum category;

        [Export] private CSharpScript actScript;
        [Export] private Resource respectiveResource;



        private bool generated = false; // <- hate this, but should work
        private Variant instance;


        public int Id => id;

        public CompressedTexture2D Icon => icon;
        public string Name => name;
        public string Description => description;
        public bool Flag 
        { 
            get 
            {
                if (actScript == null) return flag;
                return ActionScript.flag;
            } 
        }

        public ItemCategoriesEnum Category => category;

        public IItemActionScript ActionScript
        {
            get
            {
                if (actScript == null) return null;

                if (!generated)
                {
                    instance = actScript.New();
                    generated = true;
                }

                if (instance.AsGodotObject() is IItemActionScript actionInterface) return actionInterface;
                return null;
            }
        }

        //public IItemActionScript ActionScript => actScript;

        public Resource RespectiveResource => respectiveResource;

        public int Index => id;
    }
}
