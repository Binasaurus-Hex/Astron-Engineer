using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Nodes.Serialization {
	public class QuaternionConverter : JsonConverter<Quaternion> {
		
		public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer) {
			float[] quaternionArray = {value.x, value.y, value.z, value.w};
			serializer.Serialize(writer,quaternionArray);
		}

		public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue,
			JsonSerializer serializer) {
			JArray array = JArray.Load(reader);
			float[] items = array.Select(jv => (float)jv).ToArray();
			return new Quaternion(items[0],items[1],items[2],items[3]);
		}
	}
}