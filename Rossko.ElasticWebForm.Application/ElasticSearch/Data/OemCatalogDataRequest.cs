using System.ComponentModel.DataAnnotations;

namespace Rossko.ElasticWebForm.Application.ElasticSearch.Data
{
    public interface IOemCatalogRequest
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string Event { get; set; }
    }
    public class OemCatalogDataRequest: IOemCatalogRequest
    {
        [Required]
        public DateTimeOffset? StartDate { get; set; } = DateTime.Today.AddDays(-10);
        [Required]
        public DateTimeOffset? EndDate { get; set; } = DateTime.Now;
        public string Event { get; set; }
    }
}
