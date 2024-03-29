﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
namespace MovieReviewAPI.Models
{
    public class UserReviews
    {
        [Key]
        public int ReviewId { get; set; }
        public int ShowId { get; set; }
        public int UserId { get; set; }
        public double UserRating { get; set; }
        public string? UserComment { get; set; }
    }
}
