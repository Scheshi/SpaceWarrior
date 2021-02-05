using System.Collections.Generic;
using System.IO;
using System.Linq;
using Models;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


namespace Asteroids.Services
{
    public class JsonSerializator
    {
        public SerializableObjectUnit[] Deserialize(string path)
        {
            return JsonConvert.DeserializeObject<SerializableObjectUnit[]>(File.ReadAllText(path));
        }

        public string Serialize(params SerializableObjectInfo[] objects)
        {
            return JsonConvert.SerializeObject(objects);
        }
    }
}