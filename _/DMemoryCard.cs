using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using Microsoft.Xna.Framework;
using System.Text.Json;
using System.IO;

namespace Hydra
{
    class MemoryCard
    {
        internal static MemoryCard current;

        internal string path;
        internal object value;
        internal Type valueType;

        public MemoryCard(Type type)
        {
            valueType = type;
            current = this;
        }

        internal void save()
        {
            save(DateTime.Now.ToString());
        }

        internal void quickSave()
        {
            save("quickSave");
        }

        void save(string path)
        {
            string contents = JsonSerializer.Serialize(value, valueType);
            File.WriteAllText(path, contents);
        }

        internal void load(string somePath)
        {
            path = somePath;
            string json = File.ReadAllText(path);
            value = JsonSerializer.Deserialize(json, valueType);
        }

        internal void delete(string path)
        {
            File.Delete(path);
        }

        internal void pathList()
        {
            string[] list = Directory.GetFiles(Directory.GetCurrentDirectory());

            foreach (string i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}
