public class CreatePostRequest
{
    public string Content { get; set; } = "";

    public List<string> Images { get; set; } = new();
}