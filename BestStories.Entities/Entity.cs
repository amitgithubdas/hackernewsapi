namespace BestStories.Entities
{
    public class Entity
    {
        public int Id { get; set; }

        public string? Type { get; set; }

        public string? By { get; set; }

        public int Time { get; set; }

        public string? text { get; set; }

        public string? Parent { get; set; }

        public int[] kids { get; set; }
        public string? Url { get; set; }
        public int Score { get; set; }
        public string? Title { get; set; }
        public string? Parts { get; set; }
        public int Descendants { get; set; }

    }
}