using Rossko.ElasticWebForm.Data.Model;

namespace Rossko.ElasticWebForm.Application.Database
{
    public interface IDbService
    {
        public Task BulkInsert(List<OemCatalog> oemCatalog);
    }
}
