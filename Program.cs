using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace proj2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            var app = builder.Build();

            app.MapPost("/", async context =>
            {
                using var streamReader = new StreamReader(context.Request.Body);
                var json = await streamReader.ReadToEndAsync();

                var jsonObj = JObject.Parse(json);

                System.Console.WriteLine(jsonObj.Root);

                JObject jsonObject = JObject.Parse(json);

                var result = jsonObj["object1"];

                System.Console.WriteLine(result.ToString().Length);

                //dynamic jsonObject = Newtonsoft.Json. .Deserialize<dynamic>(json);

                //string object1object1Key1Value = jsonObject[(string)"object1"][(string)"object1object1"][(string)"object1object1Key1"];
                Console.WriteLine("Value of object1object1Key1: " + result);

                // Deserialize JSON into JsonDocument
                // using (JsonDocument doc = JsonDocument.Parse(json))
                // {
                //     HandleJson(doc.RootElement);
                // }
            });

            app.Run();
        }

        // Method to handle the JSON document
        private static void HandleJson(JsonElement element)
        {
            for(int x = 0; x < 3; x++)
            {
                System.Console.WriteLine("  ");
            }

            //System.Console.WriteLine(element);

            // Traverse the JSON document and handle each element
            foreach (var property in element.EnumerateObject())
            {
                if (property.Value.ValueKind == JsonValueKind.Object)
                {
                    // If the value is an object, recursively handle it
                    HandleJson(property.Value);
                }
                else if (property.Value.ValueKind == JsonValueKind.Array)
                {
                    // If the value is an array, handle each element in the array
                    foreach (var arrayElement in property.Value.EnumerateArray())
                    {
                        HandleJson(arrayElement);
                    }
                }
                else
                {
                    // Handle other value types (string, number, boolean, etc.)
                    Console.WriteLine($"Property: {property.Name}, Value: {property.Value}");
                }
            }

            for(int x = 0; x < 3; x++)
            {
                System.Console.WriteLine("  ");
            }   
        }
    }
}


//builder.Services.AddSingleton<IService, Service>();
    // public class Service : IService
    // {
    //     public void ServiceMethod()
    //     {
    //         Console.WriteLine("ServiceMethod Called");
    //     }
    // }

    // public interface IService
    // {
    //     public void ServiceMethod();
    // }