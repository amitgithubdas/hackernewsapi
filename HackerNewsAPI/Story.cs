namespace HackerNewsAPI
{
    public class Story
    {
        public string title { get; set; }
        public string? uri { get; set; }
        public string? postedby { get; set; }
        public DateTime Time { get; set; }
        public int score { get; set; }
        public int commentcount { get; set; }

    }
}