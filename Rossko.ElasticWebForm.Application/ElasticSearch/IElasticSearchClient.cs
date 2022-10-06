using Nest;
using Rossko.ElasticWebForm.Application.ElasticSearch.Data;

namespace Rossko.ElasticWebForm.Application.ElasticSearch
{
    public interface IElasticSearchClient
    {
        Task<CountResponse> GetCount(OemCatalogDataRequest request, string indexName);
        IEnumerable<IReadOnlyCollection<OemCatalogModel>> GetOemCatalogAll(OemCatalogDataRequest request, string indexName);
        Task GetCountAggregatedByData(OemCatalogDataRequest request, string indexName);
    }
}
