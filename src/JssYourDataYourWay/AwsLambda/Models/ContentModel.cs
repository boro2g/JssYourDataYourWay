namespace AwsLambda.Models
{
    public class ContentModel
    {
        public string Content { get; set; }
        public string Source { get; set; } = "AWS";
        public int DelayMs { get; set; }
    }
}
