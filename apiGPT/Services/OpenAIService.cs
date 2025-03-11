using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using apiGPT.Models;

namespace apiGPT.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAIService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenAI:ApiKey"];
        }

        public async Task<string> GetChatGPTResponse(string prompt)
        {
            var request = new ChatGPTRequest
            {
                Messages = new List<Message>
                {
                    new Message { Role = "system", Content = "You are a helpful assistant." },
                    new Message { Role = "user", Content = prompt }
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var chatGPTResponse = JsonConvert.DeserializeObject<ChatGPTResponse>(responseString);

            return chatGPTResponse?.Choices?.FirstOrDefault()?.Message?.Content ?? "No response";
        }
    }
}
