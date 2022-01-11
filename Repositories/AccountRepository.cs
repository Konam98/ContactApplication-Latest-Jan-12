using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ContactApplication.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContactApplication.Contexts;
using ContactApplication.Entities;
using System.Linq;


namespace ContactApplication.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private ApplicationDbContext context;
        public AccountRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
         public User validateUser(LoginModel login)
        {
            return context.User.SingleOrDefault(u => u.Email == login.Email && u.Password == login.Password);
        }
public FeedBack addUser(User user, Role role)
        {
            FeedBack feedback = null;
            try
            {
                //Check if Farmer already exists by matching Email
                User user1 = context.User.SingleOrDefault(s => s.Email == user.Email);
                if (user1 == null)
                {
                    //Add Farmers
                    user.Role = role.ToString();
                    context.User.Add(user);
                    context.SaveChanges();
                    feedback = new FeedBack() { Result = true, Message = "User Added" };
                }
                else
                {
                    feedback = new FeedBack() { Result = false, Message = "User with same EmailID already exists" };

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
