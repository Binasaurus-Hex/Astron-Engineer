using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Nodes.Serialization
{
    public class ColorConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            float[] vector4 = {value.r,value.g,value.b,value.a};
            serializer.Serialize(writer,vector4);
        }

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            float[] items = array.Select(jv => (float)jv).ToArray();
            return new Color(items[0],items[1],items[2],items[3]);
        }
    }
}