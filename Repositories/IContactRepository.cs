using ContactApplication.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ContactApplication.Repositories;

namespace ContactApplication.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetContacts();
        Task<int> GetContactById(int ContactId);        
        FeedBack AddContact(Contact contact);
        FeedBack UpdateContactByContactId(int ContactId);
        FeedBack DeleteContactById(int ContactId);



    }
}