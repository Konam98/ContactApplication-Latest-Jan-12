using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ContactApplication.Models;
using ContactApplication.Entities;
using ContactApplication.DTO;
using System.Collections.Generic;

namespace ContactApplication.Repositories
{
    public interface IAccountRepository
    {
        //Task<IdentityResult> SignUp(SignUp signUpModel);
       
        User ValidateUser(LoginModel login);
        FeedBack AddUser(User user, Role role);
        
        //Forgot Password method for user
        FeedBack ForgetPassword(string Email, ForgetPasswordDTO forgetPasswordDTO);


    }
}