using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MTTKDotNetCore.ConsoleApp3
{
    public  class RestClientExample
    {
        private readonly RestClient _client;
        private readonly string _endpoint = "https://jsonplaceholder.typicode.com/posts";

        public RestClientExample()
        {
            _client = new RestClient();
        }

        public async Task Read()
        {
            RestRequest request = new RestRequest(_endpoint, Method.Get);
            var response = await _client.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Edit(int id)
        {
            RestRequest request = new RestRequest($"{_endpoint}/{id}", Method.Get);
            var response = await _client.GetAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;
                Console.WriteLine(jsonStr);
            }
        }
         
        public async Task Create(int userId, string title, string body)
        {
            PostModel requestModel = new PostModel
            {
                userId = userId,
                title = title,
                body = body
            };

            RestRequest request = new RestRequest(_endpoint, Method.Post);
            request.AddJsonBody(requestModel);

            var response = await _client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content);
            }
        }

        public async Task Update(int id, int userId, string title, string body)
        {
            PostModel requestModel = new PostModel
            {
                id = id,
                userId = userId,
                title = title,
                body = body
            };

            RestRequest request = new RestRequest($"{_endpoint}/{id}", Method.Patch);
            request.AddJsonBody(requestModel);

            var response = await _client.PatchAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content);
            }
        }

        public async Task Delete(int id)
        {
            var response = await _client.DeleteAsync($"{_endpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                RestRequest request = new RestRequest($"{_endpoint}/{id}", Method.Delete);

                var jsonStr = response.Content;
                Console.WriteLine(jsonStr);
            }
        }
    }
}

public class PostModel
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}
