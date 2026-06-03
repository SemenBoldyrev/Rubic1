using Godot;
using Rubic1.Script.DialogScript.DataSets;
using Rubic1.Script.DialogScript.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript
{
    public class DialToParts : IDialToParts
    {
        public List<DialPartData> FileToList(string path)
        {
            //
            path = DisintegrateRes(path);
            //
            using FileStream stream = File.OpenRead(path);
            List<DialPartData> dialList = JsonSerializer.Deserialize<List<DialPartData>>(stream);
            return dialList;
        }

        public async Task<List<DialPartData>> FileToListAsync(string path)
        {
            //
            path = DisintegrateRes(path);
            //
            using FileStream stream = File.OpenRead(path);
            List<DialPartData> dialList = await JsonSerializer.DeserializeAsync<List<DialPartData>>(stream);
            return dialList;
        }

        private string DisintegrateRes(string path)
        {
            //somehow this thing is finding the relative part, by "res://"
            //but adds "/res:/" in middle the path, so this fix removes it manually
            //if i wont find a fix, this should help, maybe
            path = Path.GetFullPath(path);
            return path.Replace("\\res:", "");
        }
    }
}
