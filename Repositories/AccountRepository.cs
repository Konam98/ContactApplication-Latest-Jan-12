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
using ContactApplication.DTO;

namespace ContactApplication.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private ApplicationDbContext context;
        public AccountRepository(ApplicationDbContext context)
        {
            this.context = context;
        }            
        

        //Forgot Password method for User
        public FeedBack ForgetPassword(string Email, ForgetPasswordDTO forgetPasswordDTO)
        {
            User user1 = context.User.SingleOrDefault(s => s.Email == Email);
            if (user1 != null)
            {
                if (forgetPasswordDTO.answer == user1.userAnswer)
                {
                    user1.Password = forgetPasswordDTO.newPassword;
                    context.User.Update(user1);
                    context.SaveChanges();
                    FeedBack feedback = new FeedBack { Result = true, Message = "Password has been reset!" };
                    return feedback;
                }
                else
                {
                    FeedBack feedback = new FeedBack { Result = false, Message = "Incorrect Answer!" };
                    return feedback;
                }
            }
            else
            {
                FeedBack feedback = new FeedBack { Result = false, Message = "User Email not registered!" };
                return feedback;
            }
        }

         public User ValidateUser(LoginModel login)
        {
            return context.User.SingleOrDefault(u => u.Email == login.Email && u.Password == login.Password);
        }
        public FeedBack AddUser(User user, Role role)
        {
            
            try
            {
                //Check if User already exists by matching Email
                User user1 = context.User.SingleOrDefault(s => s.Email == user.Email);
                if (user1 == null)
                {
                    //Add User
                    user.Role = role.ToString();
                    context.User.Add(user);
                    context.SaveChanges();
                    var feedback = new FeedBack() { Result = true, Message = "User Added" };
                    return feedback;
                }
                else
                {
                   var feedback = new FeedBack() { Result = false, Message = "User with same EmailID already exists" };
                    return feedback;
                }

            }
            catch (Exception ex)
            {
                var feedback = new FeedBack() { Result = false, Message = ex.Message };
                return feedback;
            }
            
        }         
        

        
    }
}

