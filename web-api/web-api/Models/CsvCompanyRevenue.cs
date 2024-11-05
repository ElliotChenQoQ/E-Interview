// CsvCompanyRevenue.cs
using CsvHelper.Configuration.Attributes;

namespace WebApp.Models
{
    public class CsvCompanyRevenue
    {
        [Name("出表日期")]
        public string ReportDate { get; set; }

        [Name("資料年月")]
        public string YearMonth { get; set; }

        [Name("公司代號")]
        public string CompanyID { get; set; }

        [Name("公司名稱")]
        public string CompanyName { get; set; }

        [Name("產業別")]
        public string Industry { get; set; }

        [Name("營業收入-當月營收")]
        public decimal? CurrentMonthRevenue { get; set; }

        [Name("營業收入-上月營收")]
        public decimal? LastMonthRevenue { get; set; }

        [Name("營業收入-去年當月營收")]
        public decimal? LastYearRevenue { get; set; }

        [Name("營業收入-上月比較增減(%)")]
        public decimal? MonthGrowth { get; set; }

        [Name("營業收入-去年同月增減(%)")]
        public decimal? YearGrowth { get; set; }

        [Name("累計營業收入-當月累計營收")]
        public decimal? CumulativeMonthRevenue { get; set; }

        [Name("累計營業收入-去年累計營收")]
        public decimal? CumulativeLastYearRevenue { get; set; }

        [Name("累計營業收入-前期比較增減(%)")]
        public decimal? CumulativeGrowth { get; set; }

        [Name("備註")]
        public string Remarks { get; set; }
    }
}
