using ContactApplication.Entities;
using ContactApplication.Repositories;
using ContactApplication.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactApplication.Models;

namespace ContactApplication.Controllers{
    public class AccountSignUpController:ControllerBase
    {
        private IAccountRepository accountRepository;
        public AccountSignUpController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
            
        }
        [HttpPost]
      [Route("userSignUp")]
        public IActionResult userSignUp(User user, Role role)
        {
            FeedBack feedback = accountRepository.AddUser(user, role);
            if (feedback.Result == true) { return Ok(feedback.Message); }
            else { return BadRequest(feedback.Message); }
        }        
    }
}