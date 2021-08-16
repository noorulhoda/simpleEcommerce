﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tachy1.Models;

namespace tachy1.BusinessLogicLayer.Services
{
    public class OrderRepository
    {

        //delcaring mongo db
        private readonly IMongoDatabase _database;

        public OrderRepository(IOptions<DatabaseSettings> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                if (client != null)
                    _database = client.GetDatabase(settings.Value.Database);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to MongoDb server.", ex);
            }

        }

        public IMongoCollection<Order> orders => _database.GetCollection<Order>("Orders");

    }
}
    