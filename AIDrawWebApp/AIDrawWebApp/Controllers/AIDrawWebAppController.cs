using AIDrawWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIDrawWebApp.Controllers
{
    public class AIDrawWebAppController : Controller
    {
        private string url = "https://localhost:7239/api/AIDraw/";
        private readonly HttpClient httpClient;

        public AIDrawWebAppController()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            httpClient = new HttpClient(handler);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveDrawing([FromBody] Drawing drawing)
        {
            if(drawing == null || string.IsNullOrWhiteSpace(drawing.name))
            {
                return BadRequest(new { message = "Invalid drawing name!" });
            }

            var response = await httpClient.PostAsJsonAsync(url + "save", drawing);

            if(response.IsSuccessStatusCode)
            {
                return Ok(new { message = "Saved to database successfully!" });
            }
            else
            {
                return StatusCode(500, new { message = "Error saving to database." });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> GenerateAI([FromBody] PromptRequest request)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(
                    "https://localhost:7239/api/AIPrompt/generate", request);

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Backend Raw Response: " + content);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode,
                        new { message = "Backend returned error", details = content });
                }

                // Deserialize safely without assuming key exists
                var json = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(content);

                if (json != null && json.ContainsKey("imageBase64"))
                {
                    return Ok(new { ImageBase64 = json["imageBase64"] });
                }

                return BadRequest(new
                {
                    message = "Backend did not return ImageBase64",
                    raw = content
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Exception in MVC proxy",
                    error = ex.Message
                });
            }
        }




    }
}
