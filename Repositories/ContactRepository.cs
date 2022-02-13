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
using ContactApplication.Entities;

namespace ContactApplication.Repositories
{
    public class ContactRepository:IContactRepository
    {        
        private ApplicationDbContext context;
        public ContactRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public User ValidateUser(LoginModel login)
        {
            return context.User.SingleOrDefault(u => u.Email == login.Email && u.Password == login.Password);
        }
        public List<Contact> GetContacts()
        {
             try
            {
                List<Contact> contacts = context.Contacts.ToList(); 
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
                //Check if Contact already exists by matching Contact ID 
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
      
        public FeedBack AddContact( Contact contact)
        {
           
            try
            {
                //Check if User already exists by matching contactNumber
                Contact contact1 = context.Contacts.SingleOrDefault(s => s.ContactNumber == contact.ContactNumber);
                if (contact1 == null)
                {                                        
                    context.Contacts.Add(contact);
                    context.SaveChanges();
                   var feedback = new FeedBack() { Result = true, Message = "Contact Added" };
                   return feedback;
                }
                else
                {
                   var feedback = new FeedBack() { Result = false, Message = "Contact is already exists" };
                   return feedback;

                }

            }
            catch (Exception ex)
            {
                var feedback = new FeedBack() { Result = false, Message = ex.Message }; 
                return feedback;
                         

            }
            
        }
        
        public FeedBack UpdateContactByContactId(int ContactId,ContactDTO contactDTO )
        {
         
            try
            {
                //Check if User already exists by matching Email & ElectricityBoardId
                Contact contact1 = context.Contacts.SingleOrDefault(s => s.ContactId == ContactId);
                if (contact1 != null)
                {
                   // ContactDTO contactDTO=new ContactDTO();
                    //contact1.ContactId=contactDTO.ContactId;
                    contact1.ContactName=contactDTO.ContactName;
                    contact1.ContactEmail=contactDTO.ContactEmail;
                    contact1.ContactAddress=contactDTO.ContactAddress;
                    contact1.ContactNumber=contactDTO.ContactNumber;
                                        
                    context.Contacts.Update(contact1);
                    context.SaveChanges();
                   var feedback = new FeedBack() { Result = true, Message = "Contact Updated" };
                    return feedback;  
                }
                else
                {
                      var feedback = new FeedBack() { Result = false, Message = "Contact is not found" };
                         return feedback;  
                }

            }
            catch (Exception ex)
            {
                  var feedback = new FeedBack() { Result = false, Message = ex.Message }; 
                       return feedback;             
                   
            }     
                 
                       
            
           
        }
        
        
        //Method for Deleting Contact Details from database
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
        public FeedBack ChangePassword(string Email, ChangePasswordDTO changePasswordDTO)
        {
            User user1 = context.User.SingleOrDefault(s => s.Email == Email);
            if (user1 != null)
            {
                if (changePasswordDTO.oldPassword == user1.Password)
                {
                    user1.Password = changePasswordDTO.newPassword;
                    context.User.Update(user1);
                    context.SaveChanges();
                    FeedBack feedback = new FeedBack { Result = true, Message = "Password Changed" };
                    return feedback;
                }
                else
                {
                    FeedBack feedback = new FeedBack { Result = false, Message = "Incorrect Password" };
                    return feedback;
                }
            }
            else
            {
                FeedBack feedback = new FeedBack { Result = false, Message = "User Email not registered!" };
                return feedback;
            }
        }
        public UserDTO GetUserDetails(string Email,string Password)
        {    
            User user = context.User.SingleOrDefault(s => s.Email == Email&& s.Password==Password);                        
                //Check if Contact already exists by matching Contact ID 
                if (user != null)
                {
                UserDTO userDTO=new UserDTO();
                userDTO.UserId=user.UserId;
                userDTO.FirstName=user.FirstName;
                userDTO.LastName=user.LastName;
                userDTO.Role=user.Role;
                userDTO.MobileNumber=user.MobileNumber;
                userDTO.Email=user.Email;
                userDTO.Password=user.Password;
                if (userDTO != null)
                {
                    return userDTO;
                }
                else { return null; }
                }
                else { return null; }
                               
        }
        
    }
}