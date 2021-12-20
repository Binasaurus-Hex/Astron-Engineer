using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Nodes.Serialization
{
    public class MonoBehaviourConverter : JsonConverter<MonoBehaviour>
    {
        public override void WriteJson(JsonWriter writer, MonoBehaviour value, JsonSerializer serializer)
        {
            
        }

        public override MonoBehaviour ReadJson(JsonReader reader, Type objectType, MonoBehaviour existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}