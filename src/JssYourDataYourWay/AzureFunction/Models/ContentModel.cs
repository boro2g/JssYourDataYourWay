namespace AzureFunction.Models
{
    public class ContentModel
    {
        public string Content { get; set; }
        public string Source { get; set; } = "Azure";
        public int DelayMs { get; set; }
    }
}
