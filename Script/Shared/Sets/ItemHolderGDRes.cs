using Godot;
using Rubic1.Script.Inventory.Resources;
using Rubic1.Script.Shared.Interfaces;
using Rubic1.Script.Shared.Sets.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.Shared.Sets
{
    public class ItemHolderGDRes<T> : IItemHolder<T> where T :Resource, IHaveIndex
    {
        private Dictionary<int, T> itemDict = new();
        private string itemFolder = "";

        public ItemHolderGDRes(string folder)
        {
            this.itemFolder = folder;
            this.LoadFiles();
        }

        private void LoadFiles()
        {
            using DirAccess dir = DirAccess.Open(itemFolder);

            if (dir == null)
            {
                GD.PrintErr($"Could not open folder {itemFolder}");
                return;
            }

            dir.ListDirBegin();

            foreach (string file in dir.GetFiles())
            {

                //!!! ITS JUST NOT ASSIGNING ITEM RES SCRIPT TO RES AFTER CREATION!!!
                var resource = GD.Load(dir.GetCurrentDir() + "/" + file); // <-- returns resource but not the item res, and cant convert one to another (mainly resource to item res) 

                if (resource is not T) continue; // <
                T tres = (T)resource; // <
                itemDict.Add(tres.Index, tres); // <-- that helped (assigning as res and then checking if T)
            }

            GD.Print($"Files loaded from '{itemFolder}' folder: {itemDict.Count}");
        }

        public T GetItemByIndex(int key)
        {
            if (!itemDict.ContainsKey(key))
            {
                GD.PrintErr($"No such item id: {key}");
                return null;
            }
            return itemDict[key];
        }
    }
}
