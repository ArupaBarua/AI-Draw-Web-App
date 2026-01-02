using AIDrawWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using OpenAI;
using OpenAI.Images;

namespace AIDrawWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIPromptController : ControllerBase
    {
        private readonly string _apiKey;

        public AIPromptController(IConfiguration config)
        {
            _apiKey = config["OpenAI:ApiKey"]
                      ?? throw new Exception("OpenAI API key is missing");
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateImage([FromBody] PromptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest(new { message = "Prompt is required." });

            try
            {
                var imageClient = new ImageClient("dall-e-3", _apiKey);

                var options = new ImageGenerationOptions
                {
                    Quality = GeneratedImageQuality.High,
                    Size = GeneratedImageSize.W1024xH1024,
                    Style = GeneratedImageStyle.Natural,
                    ResponseFormat = GeneratedImageFormat.Uri
                };

                var response = await imageClient.GenerateImageAsync(request.Prompt, options);

                var imageUrl = response.Value.ImageUri;

                using var http = new HttpClient();
                var imgBytes = await http.GetByteArrayAsync(imageUrl);

                var base64 = Convert.ToBase64String(imgBytes);

                return Ok(new { ImageBase64 = base64 });
            }
            catch (Exception ex)
            {
                Console.WriteLine("IMAGE API ERROR: " + ex);

                return StatusCode(500, new
                {
                    message = "Error generating image",
                    details = ex.Message
                });
            }
        }
    }
}
