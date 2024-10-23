﻿using System.ComponentModel.DataAnnotations;

namespace CustomIdentity.Models
{
    public class LoginRequest
    {
        [EmailAddress]
        [Required]      
        public string Email { get; set; }

        [Required]        
        public string Password { get; set; }
    }
}