﻿using System;
using System.ComponentModel.DataAnnotations;
namespace MovieReviewAPI.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public int NumOfReviews { get; set; }
    }
}
