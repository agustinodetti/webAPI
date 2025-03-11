namespace apiGPT.Models
{
    public class ChatGPTResponse
    {
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
    }
}
