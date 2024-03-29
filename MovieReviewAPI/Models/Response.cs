﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Net;

namespace MovieReviewAPI.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public string? statusDescription { get; set; }
        public object? result { get; set; }
        public Response(int code, string description = null, object data = null)
        {
            statusCode = code;
            switch (statusCode)
            {
                case 200:
                    statusDescription = "Successfully retrieved " + description;
                    break;
                case 201:
                    statusDescription = "Successfully created " + description;
                    break;
                case 202:
                    statusDescription = "Successfully created " + description + " but may take longer than usual to complete";
                    break;
                case 203:
                    statusDescription = "Successfully created ";
                    break;
                case 204:
                    statusDescription = description;
                    break;
                case 400:
                    statusDescription = "Invalid request. Check your request for typos.";
                    break;
                case 404:
                    statusDescription = description + " could not be found.";
                    break;
                case 405:
                    statusDescription = "Method not allowed";
                    break;
                case 409:
                    statusDescription = "Error. The entry already exists";
                    break;
                default:
                    statusDescription = description;
                    break;
            }
            result = data;
        }
        
    }
}
