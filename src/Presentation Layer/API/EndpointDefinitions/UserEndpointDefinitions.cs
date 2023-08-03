using API.Helpers.EndpointDefinitionsHelpers;
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

        internal async Task<List<Domain.Models.User>> GetAllAsync(
            IUserRepository repo,
            ILogger<UserEndpointDefinitions> _logger)
        {
            _logger.LogInformation("log information!!!!!!");

            _logger.LogTrace("-----------> LOG TRACE <----------- ");
            _logger.LogDebug("-----------> LOG DEBUG <-----------");
            _logger.LogInformation("-----------> LOG INFORMATION <-----------");
            _logger.LogWarning("-----------> LOG WARNING <-----------");
            _logger.LogError("-----------> LOG ERROR <-----------");
            _logger.LogCritical("-----------> LOG CRITICAL/FATAL <-----------");

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
