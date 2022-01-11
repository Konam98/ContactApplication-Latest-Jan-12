using Microsoft.AspNetCore.Mvc;
using ContactApplication.Models;
using ContactApplication.Repositories;
using ContactApplication.Entities;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace ContactApplication.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("userSignUp")]
        public IActionResult UserSignUp(User user, Role role)
        {
            FeedBack feedback = accountRepository.addUser(user, role);
            if (feedback.Result == true) { return Ok(feedback.Message); }
            else { return BadRequest(feedback.Message); }
        }
        [Route("userLogin")]
        [HttpPost]
        public IActionResult userLogin(LoginModel login)
        {
            LoggedUserModel model = new LoggedUserModel();
            //Validating Login credentials
            User user = accountRepository.validateUser(login);
            if (user != null)
            {
                string token = getTokenForUser(user);
                //model = new LoggedUserModel() { Id = admin.adminId, EmailID = admin.adminEmail, Token = token, Role = admin.role };
                model = new LoggedUserModel() { EmailID = user.Email, Token = token,Role = user.Role};
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }

            return Ok(model);
        }
private string getTokenForUser(User user)
        {
            var _config = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json").Build();
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(2);
            var securityKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials
        (securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                   {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)

                   }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        
    }
}