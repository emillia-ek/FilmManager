﻿namespace MovieApp.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public int? ExternalId { get; set; }
        public required string Title { get; set; }
        public string? Director { get; set; }
        public required int Year { get; set; }
        public float Rate { get; set; }
    }
}
