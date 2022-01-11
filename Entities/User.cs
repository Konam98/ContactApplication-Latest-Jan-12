using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ContactApplication.Entities
{
    public class User 
    {
         public long UserId{get;set;}
         public string FirstName { get; set; }

        public string LastName { get; set; }    
        public string Role {get;set;}   
       

        public string MobileNumber { get; set; }
        public string Email { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}