using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace MTTKDotNetCore.RestApi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurmaProjectIdeaController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly RestClient _restClient;
        private readonly IPickAPileApi _ickAPileApi;

        public BurmaProjectIdeaController(HttpClient httpClient, RestClient restClient, IPickAPileApi ickAPileApi)
        {
            _httpClient = httpClient;
            _restClient = restClient;
            _ickAPileApi = ickAPileApi;
        }

        [HttpGet("birds")]
        public async Task<IActionResult> Birds()
        {
            var response = await _httpClient.GetAsync("");
            string str = await response.Content.ReadAsStringAsync();
            return Ok(str);
        }

        [HttpGet("bagan")]
        public async Task<IActionResult> Bangan()
        {
            RestRequest request = new RestRequest("bagan-map", Method.Get);
            var response = await _restClient.GetAsync(request);
            return Ok(response.Content);
        }

        [HttpGet("pick-a-pile")]
        public async Task<IActionResult> PickAPile()
        {
            var response = await _ickAPileApi.GetPickAPile();
            return Ok(response);
        }
    }
}
