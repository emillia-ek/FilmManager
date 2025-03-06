﻿namespace MovieApp.Models.Entities
{
    public class Movie
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? ExternalId { get; set; }
        public required string Title { get; set; }
        public required string Director { get; set; }
        public required int Year { get; set; }
    }
}
