namespace API.Helpers.EndpointDefinitionsHelpers
{
    public static class EndpointDefinitionExtensions
    {
        public static void AddEndpointDefinitions(
            this IServiceCollection services, params Type[] types
            )
        {
            var endpointDefinitions = new List<IEndpointDefinition>();

            foreach (var type in types)
            {
                endpointDefinitions.AddRange(
                    type.Assembly.ExportedTypes
                    .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) &&
                    !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IEndpointDefinition>()
                    );
            }

            foreach (var endpoint in endpointDefinitions) 
            
            {
                endpoint.DefineServices(services);
            }

            services.AddSingleton(
                endpointDefinitions as IReadOnlyCollection<IEndpointDefinition> 
                );
        }

        public static void UseEndpointDefinitions (this WebApplication app)
        {
            var definitions =  app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();
            foreach (var definition in definitions)
            {
                definition.DefineEndpoints(app);
            }
        }
    }
}
