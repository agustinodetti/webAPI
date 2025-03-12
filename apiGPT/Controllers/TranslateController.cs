using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Translation.V2;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            Console.WriteLine(JsonConvert.SerializeObject(request));
            Console.WriteLine($"Texto recibido: {request.Text}");
            Console.WriteLine($"Idioma destino: {request.TargetLanguage}");

            if (string.IsNullOrWhiteSpace(request.Text))
                return BadRequest("El texto a traducir no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(request.TargetLanguage))
                return BadRequest("Debe especificar un idioma de destino.");

            try
            {
                var response = await _translationClient.TranslateTextAsync(request.Text, request.TargetLanguage);

                if (response == null || string.IsNullOrEmpty(response.TranslatedText))
                    return StatusCode(500, "No se pudo obtener la traducción.");

                return Ok(new { TranslatedText = response.TranslatedText });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la traducción: {ex.Message}");
            }
        }

    }

    public class TranslateRequest
    {
        public string Text { get; set; }
        public string TargetLanguage { get; set; } // Ejemplo: "es" para español, "en" para inglés
    }
}
