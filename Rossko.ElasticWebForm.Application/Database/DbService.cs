using Rossko.ElasticWebForm.Data.Model;
using EFCore.BulkExtensions;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Rossko.ElasticWebForm.Application.Database
{

    public class DbService : IDbService
    {
        private readonly OemCatalogDbContext _context;
        private readonly ILogger<DbService> _logger;
        public DbService(OemCatalogDbContext context, ILogger<DbService> logger)
        {
            _logger = logger;
            _context = context;

        }


        public async Task BulkInsert(List<OemCatalog> oemCatalog)
        {
            try
            {
                await _context.BulkInsertOrUpdateAsync(oemCatalog,
                    new BulkConfig
                    {
                        UseTempDB = false,
                        BatchSize = 10000,
                        UpdateByProperties = new List<string>() { nameof(OemCatalog.HitId) },
                        PropertiesToIncludeOnUpdate = new List<string> { "" }
                    });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(message: ex.Message);
            }
        }
    }
}

