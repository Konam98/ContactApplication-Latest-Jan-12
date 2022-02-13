using ContactApplication.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ContactApplication.DTO;
using ContactApplication.Entities;

namespace ContactApplication.Repositories
{
    public interface IContactRepository
    {
        User ValidateUser(LoginModel login);
        //Task<List<Contact>> GetContacts();
        List<Contact> GetContacts();
        
        ContactDTO GetContactById(int ContactId);
       // Task<int> GetContactById(int ContactId);        
        FeedBack AddContact(Contact contact);
        
        FeedBack UpdateContactByContactId(int ContactId,ContactDTO contactDTO);
        FeedBack DeleteContactById(int ContactId);
       FeedBack ChangePassword(string Email, ChangePasswordDTO changePasswordDTO);
         UserDTO GetUserDetails(string Email,string Password);
         }
}