using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.Validation;
using System;
using System.IO;

namespace JsonSchemaCreateAndValidate
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateSchemaFile();
            
            CreateJsonFile();

            ValidateSchema();
        }

        private static void CreateSchemaFile()
        {
            var schema = JsonSchema.FromType<ExampleClass>();
            var schemaData = schema.ToJson();

            var pathName = @"C:\Temp\";
            var fileName = @"schema.json";

            using (StreamWriter file = File.CreateText(pathName + fileName))
            {
                file.Write(schemaData);
            }
        }

        private static void ValidateSchema()
        {
            var schema = JsonSchema.FromType<ExampleClass>();
            var pathName = @"C:\Temp\";
            var fileName = @"jsonfile.json";

            //read json data to string variable
            var data = File.ReadAllText(pathName + fileName);
            
            var validator = new JsonSchemaValidator();
            var result = validator.Validate(data, schema);

            Console.WriteLine($"validation messages found {result.Count}");

            foreach (var item in result)
            {
                Console.WriteLine($"Kind of validation error: {item.Kind}");
                if (item.HasLineInfo)
                {
                    Console.WriteLine($"On line {item.LineNumber} at position {item.LinePosition}");
                }
                Console.WriteLine($"Property: {item.Property}");
                Console.WriteLine($"Path: {item.Path}");
                Console.WriteLine();
            }

        }

        private static void CreateJsonFile()
        {
            var pathName = @"C:\Temp\";
            var fileName = @"jsonfile.json";

            using (StreamWriter file = File.CreateText(pathName + fileName))
            {
                JsonSerializer serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };

                ExampleClass ec = CreateExampleData();

                serializer.Serialize(file, ec);
            }
        }

        private static ExampleClass CreateExampleData()
        {
            return new ExampleClass
            {
                DateTimeValue = DateTime.Now,
                DoubleValue = 1.8,
                FloatValue = (float)1.5,
                IgnoredValue = "ignore",
                IntValue = 1,
                SchemaIgnoredValue = "schemaIgnore",
                StringValue = "stringValue"
            };
        }
    }
}
