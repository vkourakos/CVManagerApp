﻿using Microsoft.AspNetCore.Identity;

namespace CVManagerapp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AM {  get; set; }
    }
}
