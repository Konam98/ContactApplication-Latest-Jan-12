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
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "USER")]
   
    public class ContactController : ControllerBase
    {       
       private IContactRepository repository;     
        public ContactController(IContactRepository repository)
        {
            this.repository = repository;           
        }
        // [HttpGet]
        // [Route("GetContacts")]
        // public List<Contact> GetContacts()
        // {
        //     return repository.GetContacts();
        // }

        [HttpPost("AddContact")]
        public string AddContact(Contact contact)
        {
            repository.AddContact(contact);
            return "Contact Added";
       }

        [HttpGet("GetContactById")]
        public IActionResult GetContactById(int ContactId)
        {            
            var contactId =  repository.GetContactById(ContactId);

            return Ok(contactId);

        }
        
        [HttpPut("UpdateContactByContactId")]
        public IActionResult UpdateContactByContactId(int ContactId)
        {
             FeedBack feedBack= repository.UpdateContactByContactId(ContactId);
            return Ok(feedBack.Message);
        }
        [HttpDelete("DeleteContactById")]
        public IActionResult DeleteContactById(int ContactId)
        {
             FeedBack feedBack= repository.DeleteContactById(ContactId);
            return Ok(feedBack.Message);
        }
        
    }
}