using API.Helpers.EndpointDefinitionsHelpers;
using RepositoryInterfaces.Interfaces;
using Serilog;

namespace API.EndpointDefinitions
{
    public class ActionLogEndpointDefinitions : IEndpointDefinition
    {
        private readonly string _endpointURL = "/actionlog/";
        private readonly ILogger<ActionLogEndpointDefinitions> _logger;
        private readonly Serilog.ILogger _logger2;

        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet($"{_endpointURL}getall", GetAllActions);
        }

        internal async Task<List<Domain.Models.ActionLog>> GetAllActions(IActionLogRepository repo)
        {
            Log.Information("woow skata");
            //var allLogs = await repo.GetAllAsync();
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
