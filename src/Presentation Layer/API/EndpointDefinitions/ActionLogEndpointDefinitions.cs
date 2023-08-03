using API.Helpers.EndpointDefinitionsHelpers;
using RepositoryInterfaces.Interfaces;

namespace API.EndpointDefinitions
{
    public class ActionLogEndpointDefinitions : IEndpointDefinition
    {
        private readonly string _endpointURL = "/actionlog/";
        private readonly ILogger<ActionLogEndpointDefinitions> _logger;

        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet($"{_endpointURL}getall", GetAllActions);
        }

        internal async Task<List<Domain.Models.ActionLog>> GetAllActions(IActionLogRepository repo)
        {
            _logger.LogInformation("/actionlog/getall has been used {0}", "this is a test....");
            var fakeAllLogs = new List<Domain.Models.ActionLog>()
            {
                new Domain.Models.ActionLog
                {
                    Id = new Guid(),
                    TimeForRequest = 0.3m
                },
                new Domain.Models.ActionLog
                {
                    Id = new Guid(),
                    TimeForRequest = 0.4m
                },
                new Domain.Models.ActionLog
                {
                    Id = new Guid(),
                    TimeForRequest = 0.5m
                },

            };
            return fakeAllLogs;
        }

    }
}
