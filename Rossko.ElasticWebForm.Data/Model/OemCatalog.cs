using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rossko.ElasticWebForm.Data.Model
{
    public partial class OemCatalog
    {
        [Key]
        public string HitId { get; set; }
        public string? Catalog { get; set; }
        public string? CarName { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateIndex { get; set; }
        public string? Event { get; set; }
        public string? From { get; set; }
        public bool? IsInternalUser { get; set; }
        public bool? IsMobile { get; set; }
        public int? MemberId { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? VinNumber { get; set; }
    }
}
