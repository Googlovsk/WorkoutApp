﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Models.Domain
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(150)")]
        public string FullName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(7)")]
        public string Gender { get; set; }

        [PersonalData]
        public DateOnly DOB { get; set; }

    }
}