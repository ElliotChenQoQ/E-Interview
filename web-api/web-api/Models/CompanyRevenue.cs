namespace WebApp.Models
{
    // CompanyRevenue.cs
    public class CompanyRevenue
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string YearMonth { get; set; } = string.Empty;
        public string ReportDate { get; set; } = string.Empty;
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
