using Godot;
using Rubic1.Script.Inventory.Data;
using Rubic1.Script.Inventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Inventory.Resources
{
    [GlobalClass]
    public partial class ItemRes: Resource
    {
        //in case i would need it
        [Export] private int id;

        [Export] private CompressedTexture2D icon;
        [Export] private string name;
        [Export] private string description;
        [Export] private bool flag;

        [Export] private ItemCategoriesEnum category;

        [Export] private CSharpScript script;
        [Export] private Resource respectiveResource;



        public int Id => id;

        public CompressedTexture2D Icon => icon;
        public string Name => name;
        public string Description => description;
        public bool Flag => flag;

        public ItemCategoriesEnum Category => category;

        public IItemActionScript Script => script is IItemActionScript ? (IItemActionScript)script : null;
        public Resource RespectiveResource => respectiveResource;
    }
}
