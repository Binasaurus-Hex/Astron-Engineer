using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Nodes.Serialization
{
    public class Vector3Converter : JsonConverter<Vector3>
    {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            float[] vector3 = {value.x, value.y, value.z};
            serializer.Serialize(writer,vector3);
        }

        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            float[] items = array.Select(jv => (float)jv).ToArray();
            return new Vector3(items[0],items[1],items[2]);
        }
    }
}