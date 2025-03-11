using Microsoft.AspNetCore.Mvc;
using apiGPT.Services;

namespace apiGPT.Controllers
{
    [Route("api/chatgpt")]
    [ApiController]
    public class ChatGPTController : ControllerBase
    {
        private readonly OpenAIService _openAIService;

        public ChatGPTController(OpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] string prompt)
        {
            var response = await _openAIService.GetChatGPTResponse(prompt);
            return Ok(new { response });
        }
    }
}
