using Newtonsoft.Json;
using NJsonSchema.Annotations;
using System;

namespace JsonSchemaCreateAndValidate
{
    public class ExampleClass
    {
        public string StringValue { get; set; }

        public float FloatValue { get; set; }

        public double DoubleValue { get; set; }

        public DateTime DateTimeValue { get; set; }

        public int IntValue { get; set; }

        [JsonIgnore]
        public string IgnoredValue { get; set; }

        [JsonSchemaIgnore]
        public string SchemaIgnoredValue { get; set; }

    }
}