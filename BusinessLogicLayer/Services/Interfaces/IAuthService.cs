using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using tachy1.Models;

namespace tachy1.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<List<User>> GetUsers();

        Task<User> GetUser(string userName, string password);
        Task AddUser(User model);
    }
}