using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ContactApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace ContactApplication.Entities
{
    public class User 
    {
        public long UserId{get;set;}

        [Required(ErrorMessage = "Please Enter your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter your Last Name")]         
        public string LastName { get; set; }    
        public string Role{get;set;}
       
        [Required]
        public string userQuestion { get; set; } //Security Question Of User

        [Required]
        public string userAnswer { get; set; } //Security Answer Of User
        
        [Required(ErrorMessage = "Please Enter your Contanct Number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Please Enter an Email address")]      
        public string Email { get; set; }
        
        [Required]
        
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Confirm Password required")]
    [CompareAttribute("NewPassword", ErrorMessage = "Password doesn't match.")]        
        public string ConfirmPassword { get; set; }

        // public List<Contact> Contacts {get;set;}
    }
}