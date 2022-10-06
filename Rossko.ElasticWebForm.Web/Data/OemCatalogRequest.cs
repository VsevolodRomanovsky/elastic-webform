using System.ComponentModel.DataAnnotations;

namespace Rossko.ElasticWebForm.Web.Data;

public class OemCatalogRequest
{
    [Required] 
    public DateTimeOffset? StartDate { get; set; } = DateTime.Today.AddDays(-10);
    [Required]
    public DateTimeOffset? EndDate { get; set; } = DateTime.Now;
    public string Event { get; set; }
}