using WebApp.Models;

namespace WebApp.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyRevenue>> GetCompanyRevenuesAsync(int? companyId);
        Task<string> GetCsvDataAsync(string url);
        List<CsvCompanyRevenue> ParseCsvData(string csvData);
        List<InsertRevenue> ConvertToInsertRevenue(List<CsvCompanyRevenue> csvRecords);
        Task InsertRevenuesAsync(List<InsertRevenue> records);
        Task SyncRevenueDataAsync();
    }
}
