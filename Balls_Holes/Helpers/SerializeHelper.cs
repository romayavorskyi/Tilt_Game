using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Tilt_Game.Helpers
{
    public static class SerializeHelper
    {
        public static void Serialize<T>(T f, string path)
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();
            using var sw = new StreamWriter(path);
            using JsonWriter writer = new JsonTextWriter(sw);
            writer.Formatting = Formatting.Indented;
            serializer.Serialize(writer, f);
        }

        public static T Deserialize<T>(string path)
        {
            using var file = File.OpenText(path);
            var serializer = new Newtonsoft.Json.JsonSerializer();
            var settings = (T)serializer.Deserialize(file, typeof(T));
            return settings;
        }
    }
}
