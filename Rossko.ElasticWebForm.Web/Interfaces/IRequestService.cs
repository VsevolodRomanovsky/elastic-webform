using Nest;
using Rossko.ElasticWebForm.Application.ElasticSearch.Data;
using Rossko.ElasticWebForm.Web.Data;

namespace Rossko.ElasticWebForm.Web.Interfaces;

public interface IRequestService<TRequest>
{
    Task<IEnumerable<IReadOnlyCollection<OemCatalogModel>>> GetAsync(TRequest request);
    Task<CountResponse> GetCountAsync(TRequest request);
    Task ImportAsync(IEnumerable<OemCatalogModel> item);

    Task TestAgg(OemCatalogRequest request);

}