using Newtonsoft.Json;

namespace Nodes.Serialization
{
    public class UnitySerialisationSettings : JsonSerializerSettings
    {
        public UnitySerialisationSettings() : base()
        {
            Converters.Add(new Vector3Converter());
            Converters.Add(new ColorConverter());
            Converters.Add(new QuaternionConverter());
        }
    }
}