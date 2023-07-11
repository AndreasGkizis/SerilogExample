using API.Helpers.EndpointDefinitionsHelpers;
using RepositoryInterfaces.Interfaces;

namespace API.EndpointDefinitions
{
    public class ActionLogEndpointDefinitions : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/actionlog/getall", GetAllActions);
        }

        internal async Task<List<Domain.Models.ActionLog>> GetAllActions(IActionLogRepository repo)
        {
            var allLogs = await repo.GetAllAsync();
            return allLogs;
        }

        public void DefineServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
