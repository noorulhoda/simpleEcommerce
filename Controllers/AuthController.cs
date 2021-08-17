using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using tachy1.Infra;
using tachy1.BusinessLogicLayer.Services.Interfaces;
using tachy1.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace rest.Controllers
{
    [Produces("application/json")]
    
    public class AuthController : Controller
    {
        private readonly IAuthService _AuthService;

        public AuthController(IAuthService AuthService)
        {
            _AuthService = AuthService;
        }

        // POST api/Auth
        [HttpPost]
        [Route("api/Auth/login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var usr = await _AuthService.GetUser(user.UserName, user.Password);

            if (usr != null)
            {

                var token = new JwtTokenBuilder()
                                    .AddSecurityKey(JwtSecurityKey.Create("key-value-token-expires"))
                                    .AddSubject(user.UserName)
                                    .AddIssuer("issuerTest")
                                    .AddAudience("bearerTest")
                                    .AddClaim("MembershipId", "111")
                                    .AddExpiry(1)
                                    .Build();

                return Ok(token.Value);

            }
            else
                return Unauthorized();
        }

        [HttpPost]
        [Route("api/Auth/register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            

            if (user != null)
            {
                await _AuthService.AddUser(user);

                if (user.Role == Role.Admin)
                {
                    var token = new JwtTokenBuilder()
                                   .AddSecurityKey(JwtSecurityKey.Create("key-value-token-expires"))
                                   .AddSubject(user.UserName)
                                   .AddIssuer("issuerTest")
                                   .AddAudience("bearerTest")
                                   .AddClaim("MembershipId", "111")
                                   .AddClaim("AdminId","222")
                                   .AddExpiry(1)
                                   .Build();
                    return Ok(token.Value);
                }
                else
                {
                    var token = new JwtTokenBuilder()
                                    .AddSecurityKey(JwtSecurityKey.Create("key-value-token-expires"))
                                    .AddSubject(user.UserName)
                                    .AddIssuer("issuerTest")
                                    .AddAudience("bearerTest")
                                    .AddClaim("MembershipId", "111")
                                    .AddExpiry(1)
                                    .Build();
                    return Ok(token.Value);
                }
               

            }
            else
                return Unauthorized();
        }


        [HttpGet]
        [Route("api/myorders/{id}")]
        public Task<ICollection<Order>> GetMyOrders(string id)
        {
            return _AuthService.GetMyOrders(id);
        }

    }
        




  
}