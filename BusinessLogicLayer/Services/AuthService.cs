using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using tachy1.Models;
using tachy1.BusinessLogicLayer.Services.Interfaces;
using tachy1.BusinessLogicLayer.Repositories;

namespace tachy1.BusinessLogicLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepository _context = null;

        public AuthService()
        {
        }

        public AuthService(IOptions<DatabaseSettings> settings)
        {
            _context = new AuthRepository(settings);
        }

        public async Task<List<User>> GetUsers()
        {
            var builder = Builders<User>.Filter;

            return await _context.Users.Find(_ => true).ToListAsync(); ;
        }

        public async Task<User> GetUser(string userName, string password)
        {

            var builder = Builders<User>.Filter;
            var filter = builder.Eq("UserName", userName) & builder.Eq("Password", password);

            return await _context.Users
                        .Find(filter)
                        .FirstOrDefaultAsync();
        }

        public async Task AddUser(User model)
        {
            //inserting data
            await _context.Users.InsertOneAsync(model);
        }

    }
}