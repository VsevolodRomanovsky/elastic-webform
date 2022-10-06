using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Rossko.ElasticWebForm.Application.ElasticSearch
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ElasticSearch:Uri"];
            var userName = configuration["ElasticSearch:Username"];
            var password = configuration["ElasticSearch:Password"];

            var settings = new ConnectionSettings(new Uri(url))
                .BasicAuthentication(userName, password).EnableDebugMode().DisableDirectStreaming();
            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
