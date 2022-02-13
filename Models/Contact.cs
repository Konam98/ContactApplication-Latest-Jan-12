using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace ContactApplication.Models

{
   // [Table]
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        public string ContactName { get; set; }       
        
        public long ContactNumber { get; set; }

        public string ContactEmail { get; set; }

        public string ContactAddress { get; set; }
        // public string ProfileImageName { get; set; }
        // [NotMapped]
        // public IFormFile ProfileImageFile { get; set; }
         

         [ForeignKey("UserId")]
         public long UserId{get;set;}


 

       

    }

}