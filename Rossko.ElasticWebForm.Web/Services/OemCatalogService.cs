using Rossko.ElasticWebForm.Web.Data;
using Rossko.ElasticWebForm.Web.Interfaces;
using Rossko.ElasticWebForm.Data.Model;
using Rossko.ElasticWebForm.Application.ElasticSearch;
using Rossko.ElasticWebForm.Application.Database;
using Rossko.ElasticWebForm.Common;
using AutoMapper;
using Rossko.ElasticWebForm.Application.ElasticSearch.Data;
using Nest;

namespace Rossko.ElasticWebForm.Web.Services;

public class OemCatalogService : IRequestService<OemCatalogRequest>
{
    private readonly IElasticSearchClient _elasticSearchClient;
    private readonly IDbService _dbService;
    private readonly IMapper _mapper;

    public OemCatalogService(IElasticSearchClient elasticSearchClient, IDbService dbService, IMapper mapper)
    {
        _elasticSearchClient = elasticSearchClient;
        _dbService = dbService;
        _mapper = mapper;
    }

    public async Task TestAgg(OemCatalogRequest request)
    {
        var indexName = IndexHelper.GetIndexList(request.StartDate);
        var appRequestData = _mapper.Map<OemCatalogDataRequest>(request);

        await _elasticSearchClient.GetCountAggregatedByData(appRequestData, indexName);
    }

    public async Task<CountResponse> GetCountAsync(OemCatalogRequest request)
    {
        var indexName = IndexHelper.GetIndexList(request.StartDate);
        var appRequestData = _mapper.Map<OemCatalogDataRequest>(request);

        return await _elasticSearchClient.GetCount(appRequestData, indexName);
    }

    public async Task<IEnumerable<IReadOnlyCollection<OemCatalogModel>>> GetAsync(OemCatalogRequest request)
    {
        var indexName = IndexHelper.GetIndexList(request.StartDate);
        var appRequestData = _mapper.Map<OemCatalogDataRequest>(request);
        
        var result = _elasticSearchClient.GetOemCatalogAll(appRequestData, indexName);

        return result;
    }

    public async Task ImportAsync(IEnumerable<OemCatalogModel> item)
    {
        await _dbService.BulkInsert(item.Map());
    }
}

public static class СustomMaping
{
    public static List<OemCatalog> Map(this IEnumerable<OemCatalogModel> model)
    {
        return model.Select(item => new OemCatalog
        {
            HitId = item.HitId,
            From = item.From,
            CarName = item.CarName,
            Catalog = item.Catalog,
            Event = item.Event,
            IsInternalUser = item.IsInternalUser,
            IsMobile = item.IsMobile,
            MemberId = item.MemberId,
            Timestamp = item.Timestamp.ConvertLongToDateTime(),
            VinNumber = item.VinNumber,
            DateIndex = item.Timestamp.ConvertLongToDateTime().Date,
        }).ToList();
    }
}