using System;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewAPI.Models
{
    public class TVShows
    {
        [Key]
        public int ShowId { get; set; }
        public string ShowName { get; set; }
        public string ShowDesc { get; set; }
        public string Genre { get; set; }
        public int NumSeasons { get; set; }
        public int NumEpisodes { get; set; }
        public int EpisodeLength { get; set; }
        public int YearReleased { get; set; }
        public bool Ongoing { get; set; }
        public double RTrating { get; set; }
        public double IMDBrating { get; set; }
        public double AVGUserRating { get; set; }
    }
}
