using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using ContactApplication.Controllers;
using ContactApplication.DTO;
using ContactApplication.Entities;
using ContactApplication.Models;
using ContactApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ContactApplication.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountLoginController : ControllerBase{

        private IAccountRepository accountRepository = null;
        
        public AccountLoginController(IAccountRepository accountRepository)
         {
            this.accountRepository = accountRepository;
         }
        [Route("UserLogin")]
        [HttpPost]
        public IActionResult UserLogin(LoginModel login)
        {
            LoggedUserModel model = new LoggedUserModel();
            //Validating Login credentials
            User user = accountRepository.ValidateUser(login);
            if (user != null)
            {
                string token = getTokenForUser(user);
                model = new LoggedUserModel() { EmailID = user.Email, Token = token, Role = user.Role };
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }

            return Ok(model);
        }
        [Route("ForgetPassword")]
        [HttpPost]
        public IActionResult ForgetPassword(Role role, string Email, ForgetPasswordDTO forgetPassword)
        {
            if (role == Role.USER)
            {
                FeedBack feedback = accountRepository.ForgetPassword(Email, forgetPassword);
                if (feedback.Result == true)
                {
                    return Ok(feedback.Message);
                }
                else
                {
                    return BadRequest(feedback.Message);
                }
            }
            else
            {
                return BadRequest("You don't have permission for this Request!");
            }
        }
        

    
        //Getting token for Authorization for User
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

            //    var token = new JwtSecurityToken(issuer: issuer,
            //audience: audience,

            //expires: DateTime.Now.AddMinutes(120),
            //signingCredentials: credentials);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                   {

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
