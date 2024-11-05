using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InsertRevenueAsync(InsertRevenue revenueData)
        {
            var parameters = new[]
            {
                new SqlParameter("@CompanyID", revenueData.CompanyID),
                new SqlParameter("@CompanyName", revenueData.CompanyName),
                new SqlParameter("@Industry", revenueData.Industry),
                new SqlParameter("@YearMonth", revenueData.YearMonth),
                new SqlParameter("@ReportDate", revenueData.ReportDate),
                new SqlParameter("@CurrentMonthRevenue", revenueData.CurrentMonthRevenue ?? (object)DBNull.Value),
                new SqlParameter("@LastMonthRevenue", revenueData.LastMonthRevenue ?? (object)DBNull.Value),
                new SqlParameter("@LastYearRevenue", revenueData.LastYearRevenue ?? (object)DBNull.Value),
                new SqlParameter("@MonthGrowth", revenueData.MonthGrowth ?? (object)DBNull.Value),
                new SqlParameter("@YearGrowth", revenueData.YearGrowth ?? (object)DBNull.Value),
                new SqlParameter("@CumulativeMonthRevenue", revenueData.CumulativeMonthRevenue ?? (object)DBNull.Value),
                new SqlParameter("@CumulativeLastYearRevenue", revenueData.CumulativeLastYearRevenue ?? (object)DBNull.Value),
                new SqlParameter("@CumulativeGrowth", revenueData.CumulativeGrowth ?? (object)DBNull.Value),
                new SqlParameter("@Remarks", revenueData.Remarks)
            };

            var sql = "EXEC InsertRevenue @CompanyID, @CompanyName, @Industry, @YearMonth, @ReportDate, @CurrentMonthRevenue, " +
                      "@LastMonthRevenue, @LastYearRevenue, @MonthGrowth, @YearGrowth, " +
                      "@CumulativeMonthRevenue, @CumulativeLastYearRevenue, @CumulativeGrowth, @Remarks";
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<IEnumerable<CompanyRevenue>> GetCompanyRevenuesAsync(int? companyId) =>
            await _context.CompanyRevenues
                .FromSqlRaw("EXEC SelectRevenueByCompanyID @CompanyID", new SqlParameter("@CompanyID", companyId ?? (object)DBNull.Value))
                .ToListAsync();
    }
}
