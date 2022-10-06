using Microsoft.Extensions.Configuration;
using Rossko.ElasticWebForm.Application.ElasticSearch.Data;
using Nest;
using Rossko.ElasticWebForm.Common;

namespace Rossko.ElasticWebForm.Application.ElasticSearch
{
    public class ElasticSearchClient : IElasticSearchClient
    {
        protected readonly IConfiguration Configuration;
        private readonly IElasticClient _elasticClient;

        public ElasticSearchClient(IConfiguration configuration, IElasticClient elasticClient)
        {
            Configuration = configuration;
            _elasticClient = elasticClient;
        }

        public async Task GetCountAggregatedByData(OemCatalogDataRequest request, string indexName)
        {
            var sd = request.StartDate.ConvertToTimestamp();
            var ed = request.EndDate.ConvertToTimestamp();

            var response = await _elasticClient.SearchAsync<OemCatalogModel>(c=>c.Index(indexName)
                .Query(q => q
                                .DateRange(p => p
                                    .Field(t => t.Timestamp).Format("epoch_second")
                                    .GreaterThanOrEquals(sd)
                                    .LessThanOrEquals(ed)
                                ) &&
                            q.Match(m => m
                                .Field(f => f.Event)
                                .Query(request.Event)))
                
                .Aggregations(agg => agg
                    .Terms("sources", t=>t
                            .Field(f => f.Timestamp)
                            .Aggregations(aa=>aa
                                .ValueCount("items_count", vc=>vc
                                    .Field(f=>f.Timestamp).Format("yyyy-MM-dd")))
                    )).Size(100).Scroll("60s"));

            //var debug = response.DebugInformation;

            while (response.Documents.Any())
            {
                var res = response.Documents;
                response = _elasticClient.Scroll<OemCatalogModel>("60s", response.ScrollId);
            }
            await _elasticClient.ClearScrollAsync(new ClearScrollRequest(response.ScrollId));
        }

        /// <summary>
        /// Count OEM Catalog 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<CountResponse> GetCount(OemCatalogDataRequest request, string indexName)
        {
            var sd = request.StartDate.ConvertToTimestamp();
            var ed = request.EndDate.ConvertToTimestamp();

            var count = await _elasticClient.CountAsync<OemCatalogModel>(c => c.Index(indexName)
                    .Query(q => q
                        .DateRange(p => p
                            .Field(t => t.Timestamp).Format("epoch_second")
                                 .GreaterThanOrEquals(sd)
                                 .LessThanOrEquals(ed)
                                 ) && 
                                q.Match(m => m
                                    .Field(f => f.Event)
                                    .Query(request.Event))));
            return count;
        }

        /// <summary>
        /// Scroll OEM Catalog 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public IEnumerable<IReadOnlyCollection<OemCatalogModel>> GetOemCatalogAll(OemCatalogDataRequest request, string indexName)
        {
            var sd = request.StartDate.ConvertToTimestamp();
            var ed = request.EndDate.ConvertToTimestamp();

            var scanResults = _elasticClient.Search<OemCatalogModel>(s => s.Index(indexName)
                .Source(sf => sf
                        .Includes(i => i
                            .Fields(
                                        f => f.Event,
                                        f => f.MemberId,
                                        f => f.IsInternalUser,
                                        f => f.IsMobile,
                                        f => f.VinNumber,
                                        f => f.Timestamp,
                                        f => f.From,
                                        f => f.Catalog,
                                        f => f.CarName
                            )
                        )
                    )
                        .Query(q => q.DateRange(p => p.Field(t => t.Timestamp).Format("epoch_second")
                                     .GreaterThanOrEquals(sd)
                                     .LessThanOrEquals(ed)
                                     ) && q.Match(m => m.Field(f => f.Event).Query(request.Event)))
                    .Size(10000).Scroll("60s"));

            while (scanResults.Documents.Any())
            {
                
                var hits = scanResults.Hits;
                foreach (var hit in hits)
                {
                    hit.Source.HitId = $"{hit.Id}";
                }

                yield return scanResults.Documents;
                scanResults = _elasticClient.Scroll<OemCatalogModel>("60s", scanResults.ScrollId);
            }
            _elasticClient.ClearScrollAsync(new ClearScrollRequest(scanResults.ScrollId));
        }
    }
}
