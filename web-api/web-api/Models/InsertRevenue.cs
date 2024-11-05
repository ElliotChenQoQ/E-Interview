namespace WebApp.Models
{
    public class InsertRevenue
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public string YearMonth { get; set; } = string.Empty;
        public string ReportDate { get; set; }
        public decimal? CurrentMonthRevenue { get; set; }
        public decimal? LastMonthRevenue { get; set; }
        public decimal? LastYearRevenue { get; set; }
        public decimal? MonthGrowth { get; set; }
        public decimal? YearGrowth { get; set; }
        public decimal? CumulativeMonthRevenue { get; set; }
        public decimal? CumulativeLastYearRevenue { get; set; }
        public decimal? CumulativeGrowth { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }
}
