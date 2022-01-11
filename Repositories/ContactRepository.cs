using System.Collections.Generic;
using System.Threading.Tasks;
using ContactApplication.Models;
using ContactApplication.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ContactApplication.DTO;

namespace ContactApplication.Repositories
{
    public class ContactRepository
    {        
        private ApplicationDbContext context;
        public ContactRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<Contact> GetContacts()
        {
            try
            {
                List<Contact> contacts = context.Contacts.FromSqlRaw("sp_GetContacts").ToList(); //implemented stored procedure
                return contacts;
            }
            catch (Exception)
            {
                return null;
            }
        }
         public ContactDTO GetContactById(int ContactId)
        {    
            Contact contact = context.Contacts.SingleOrDefault(s => s.ContactId == ContactId);                        
                //Check if Customer already exists by matching Email & ElectricityBoardId
                if (contact != null)
                {
                ContactDTO contactDTO=new ContactDTO();
                contactDTO.ContactId=contact.ContactId;
                contactDTO.ContactName=contact.ContactName;
                contactDTO.ContactEmail=contact.ContactEmail;
                contactDTO.ContactAddress=contact.ContactAddress;
                if (contactDTO != null)
                {
                    return contactDTO;
                }
                else { return null; }
                }
                else { return null; }
                               
        }
      
        public FeedBack AddContact(Contact contact)
        {
            FeedBack feedback = null;
            try
            {
                //Check if Customer already exists by matching Email & ElectricityBoardId
                Contact contact1 = context.Contacts.SingleOrDefault(s => s.ContactNumber == contact.ContactNumber);
                if (contact1 == null)
                {
                    //Add Supplier
                    //supplier.Role = role.ToString();
                    context.Contacts.Add(contact);
                    context.SaveChanges();
                    feedback = new FeedBack() { Result = true, Message = "Contact Added" };
                }
                else
                {
                    feedback = new FeedBack() { Result = false, Message = "Contact is already exists" };

                }

            }
            catch (Exception ex)
            {
                feedback = new FeedBack() { Result = false, Message = ex.Message };

            }
            return feedback;
        }
        
        public FeedBack UpdateContactByContactId(int ContactId)
        {
            FeedBack feedback = null;
            try
            {
                //Check if Customer already exists by matching Email & ElectricityBoardId
                Contact contact1 = context.Contacts.SingleOrDefault(s => s.ContactId == ContactId);
                if (contact1 != null)
                {
                    //Add Supplier
                    //supplier.Role = role.ToString();
                    context.Contacts.Update(contact1);
                    context.SaveChanges();
                    feedback = new FeedBack() { Result = true, Message = "Contact Updated" };
                }
                else
                {
                    feedback = new FeedBack() { Result = false, Message = "Contact is not found" };

                }

            }
            catch (Exception ex)
            {
                feedback = new FeedBack() { Result = false, Message = ex.Message };

            }
            return feedback;
        }
        
        //Method for Deleting Crop Details from database
        public FeedBack DeleteContactById(int ContactId)
        {
            FeedBack feedback = null;
            try
            {                
                Contact contact1 = context.Contacts.SingleOrDefault(s => s.ContactId == ContactId);
                if (contact1 != null)
                {                    
                    context.Contacts.Remove(contact1);
                    context.SaveChanges();
                    feedback = new FeedBack() { Result = true, Message = "Contact Deleted" };
                }
                else
                {
                    feedback = new FeedBack() { Result = false, Message = "Contact is not found" };

                }

            }
            catch (Exception ex)
            {
                feedback = new FeedBack() { Result = false, Message = ex.Message };

            }
            return feedback;
        }
        
    }
}