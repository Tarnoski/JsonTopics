using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonTopics
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Generate ServiceProvider
            var services = new ServiceCollection();
            services.AddHttpClient();
            var serviceProvider = services.BuildServiceProvider();

            //Create HttpClient with the IHttpClientFactory service
            var clientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var client = clientFactory.CreateClient();

            //GET request
            Uri api = new Uri("https://api.sampleapis.com/codingresources/codingResources");
            var response = await client.GetAsync(api);
            response.EnsureSuccessStatusCode();

            //Deserialize Json array to List of CodingResource
            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonArray = JsonSerializer.Deserialize<List<CodingResource>>(responseBody);

            //Print topics of List in the Console
            foreach (var obj in jsonArray)
                foreach (var topic in obj.topics)
                    Console.WriteLine(topic);

            //Stops console from closing after displaying the topics
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
