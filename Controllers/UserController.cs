using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactApplication.Repositories;
using ContactApplication.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ContactApplication.DTO;
using ContactApplication.Entities;
using System;
namespace ContactApplication.Controllers
{

   [Route("api/[controller]")]
   [ApiController]   
   [Authorize(Roles = "USER")]   
    public class UserController : ControllerBase
    {       
       private IContactRepository repository;     
        public UserController(IContactRepository repository)
        {
            this.repository = repository;           
        }
        [HttpGet]
        [Route("GetContacts")]
        public List<Contact> GetContacts()
        {
            return repository.GetContacts();
        }

        [HttpPost("AddContact")]
        public IActionResult AddContact(Contact contact)
        {
            FeedBack feedBack= repository.AddContact(contact);            
            return Ok(feedBack.Message);
       }

        [HttpGet("GetContactById")]
        public IActionResult GetContactById(int ContactId)
        {            
            var contactId =  repository.GetContactById(ContactId);

            return Ok(contactId);

        }
        
        [HttpPut("UpdateContactByContactId")]
        public IActionResult UpdateContactByContactId(int ContactId,ContactDTO contactDTO)        
        {
            //  var contactId=repository.UpdateContactByContactId(ContactId);
            //  return Ok(contactId);
             FeedBack feedBack= repository.UpdateContactByContactId(ContactId,contactDTO);
            return Ok(feedBack.Message);
        }
        [HttpDelete("DeleteContactById")]
        public IActionResult DeleteContactById(int ContactId)
        {
             FeedBack feedBack= repository.DeleteContactById(ContactId);
            return Ok(feedBack.Message);
        }
        
        [HttpPost]
    [Route("ChangePassword")]
        public IActionResult ChangePassword(string Email, ChangePasswordDTO changePasswordDTO)
        {
            FeedBack feedback = repository.ChangePassword(Email, changePasswordDTO);
            if (feedback.Result == true) { return Ok(feedback.Message); }
            else { return NotFound(feedback.Message); }

        }
        [HttpGet]
        [Route("GetUsers")]
        public UserDTO GetUserDetails(string Email,string Password)
        {
            return repository.GetUserDetails(Email,Password);
        }
        
    }
}