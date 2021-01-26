using Newtonsoft.Json;


namespace Models
{
    public struct SerializableObjectUnit
    {
        [JsonProperty(PropertyName = "unit")]
        public SerializableObjectInfo Unit { get; set; }
    }
}