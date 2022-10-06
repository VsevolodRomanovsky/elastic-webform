namespace Rossko.ElasticWebForm.Common
{
    public static class IndexHelper
    {
        //TODO: реализовать для нескольких индексов
        public static string GetIndexList(DateTimeOffset? startDate)
        {
            var month = startDate.Value.Date.Month;
            var indexName = $"oem_catalog-{month}.{startDate.Value.Date.Year}";

            if (month < 10)
            {
                indexName = $"oem_catalog-0{month}.{startDate.Value.Date.Year}";
            }

            return indexName;
        }

        public static DateTime ConvertLongToDateTime(this long unixDate)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = start.AddSeconds(unixDate).ToLocalTime();
            return date;
        }

        public static string ConvertToTimestamp(this DateTimeOffset? dateTime)
        {
            return dateTime.Value.ToUnixTimeSeconds().ToString();
        }
    }
}
