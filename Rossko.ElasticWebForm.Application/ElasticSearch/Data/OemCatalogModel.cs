using Nest;

namespace Rossko.ElasticWebForm.Application.ElasticSearch.Data
{
    public class OemCatalogModel
    {
        public string HitId { get; set; }

        [Text(Name = "event")]
        public string Event { get; set; }

        [Number(Name = "member_id")]
        public int MemberId { get; set; }

        [Boolean(Name = "is_internal_user", NullValue = false, Store = true)]
        public bool IsInternalUser { get; set; }

        [Boolean(Name= "is_mobile", NullValue = false, Store = true)]
        public bool IsMobile { get; set; }

        [Text(Name="vin_number")]
        public string VinNumber { get; set; }

        [Date(Format = DateFormat.epoch_second, Name = "timestamp")]
        public long Timestamp { get; set; }

        [Text(Name = "from")]
        public string From { get; set; }

        [Text(Name = "catalog")]
        public string Catalog { get; set; }

        [Text(Name = "car_name")]
        public string CarName { get; set; }


    }
}
