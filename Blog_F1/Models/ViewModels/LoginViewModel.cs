﻿namespace Blog_F1.Models.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
