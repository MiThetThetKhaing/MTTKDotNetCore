using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MTTKDotNetCore.ConsoleApp3;

public class HttpClientExample
{
    private readonly HttpClient _client;
    private readonly string _endpoint = "https://jsonplaceholder.typicode.com/posts";

    public HttpClientExample()
    {
        _client = new HttpClient();
    }

    public async Task Read()
    {
        var response = await _client.GetAsync(_endpoint);
        if (response.IsSuccessStatusCode)
        {
            var jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }

    public async Task Edit(int id)
    {
        var response = await _client.GetAsync($"{_endpoint}/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("No data found.");
            return;
        }
        if (response.IsSuccessStatusCode)
        {
            var jsonStr = await response.Content.ReadAsStringAsync();
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

        var jsonReq = JsonConvert.SerializeObject(requestModel);
        var content = new StringContent(jsonReq, Encoding.UTF8, Application.Json); // "application/json"
        var response = await _client.PostAsync(_endpoint, content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync()); 
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

        var jsonReq = JsonConvert.SerializeObject(requestModel);
        var content = new StringContent(jsonReq, Encoding.UTF8, Application.Json);
        var response = await _client.PatchAsync($"{_endpoint}/{id}", content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
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
            var jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
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

