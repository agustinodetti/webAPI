using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Translation.V2;
using System.Threading.Tasks;

namespace apiGPT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TranslateController : ControllerBase
    {
        private readonly TranslationClient _translationClient;

        public TranslateController(TranslationClient translationClient)
        {
            _translationClient = translationClient;
        }

        [HttpPost("traducir")]
        public async Task<IActionResult> Translate([FromBody] TranslateRequest request)
        {
            if (string.IsNullOrEmpty(request.Text))
                return BadRequest("El texto a traducir no puede estar vacío.");

            var response = await _translationClient.TranslateTextAsync(request.Text, request.TargetLanguage);

            return Ok(new { TranslatedText = response.TranslatedText });
        }
    }

    public class TranslateRequest
    {
        public string Text { get; set; }
        public string TargetLanguage { get; set; } // Ejemplo: "es" para español, "en" para inglés
    }
}
