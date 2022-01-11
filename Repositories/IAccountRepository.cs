using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ContactApplication.Models;
using ContactApplication.Entities;


namespace ContactApplication.Repositories
{
    public interface IAccountRepository
    {
        //Task<IdentityResult> SignUp(SignUp signUpModel);
       
         User validateUser(LoginModel login);
        FeedBack addUser(User user, Role role);

    }
}