﻿using API.Helpers.EndpointDefinitionsHelpers;
using RepositoryInterfaces.Interfaces;
using Serilog;

namespace API.EndpointDefinitions
{
    public class UserEndpointDefinitions : IEndpointDefinition
    {
        public string _endpointURL = "/user/";
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet($"{_endpointURL}getall", GetAllAsync).WithName("GetAll");
        }

        internal async Task<List<Domain.Models.User>> GetAllAsync(IUserRepository repo)
        {
            Log.Debug("This is a Log I custom called ");
            Log.Information("This is a Log I custom called ");
            Log.Warning("This is a Log I custom called ");
            Log.Error("This is a Log I custom called ");
            Log.Fatal("This is a Log I custom called ");
            //var allLogs = await repo.GetAllAsync();
            var fakeUsers = new List<Domain.Models.User>()
            {
                new Domain.Models.User
                {
                    Id = new Guid(),
                    Name = "andreas",
                    Lastname = "gkizis",
                    Email= "andreas@gmail.com"
                },
               new Domain.Models.User
                {
                    Id = new Guid(),
                    Name = "giorgos",
                    Lastname = "papadoloulos",
                    Email= "giorgos@gmail.com"
                },
               new Domain.Models.User
                {
                    Id = new Guid(),
                    Name = "kwstas",
                    Lastname = "georgiou",
                    Email= "kwstas@gmail.com"
                },
              
            };
            return fakeUsers;
        }
    }
}
