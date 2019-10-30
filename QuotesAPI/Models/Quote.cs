using System;

namespace QuotesAPI.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public Quote()
        {
        }

        public Quote(int id, string title, string author, string description)
        {
            Id = id;
            Title = title;
            Author = author;
            Description = description;
        }
    }
}
