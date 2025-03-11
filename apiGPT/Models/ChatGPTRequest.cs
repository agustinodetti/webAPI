namespace apiGPT.Models
{
    public class ChatGPTRequest
    {
        public string Model { get; set; } = "gpt-3.5-turbo";
        public List<Message> Messages { get; set; }
        public double Temperature { get; set; } = 0.7;
        public int MaxTokens { get; set; } = 100;
    }

    public class Message
    {
        public string Role { get; set; } = "user";
        public string Content { get; set; }
    }
}
