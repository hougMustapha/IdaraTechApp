﻿using System.ComponentModel.DataAnnotations;

namespace IdaraTechApi.DTOs.Account
{
    public class LoginWithExternalDto
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Provider { get; set; }
    }
}
