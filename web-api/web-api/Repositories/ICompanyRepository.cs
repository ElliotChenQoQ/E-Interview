using WebApp.Models;

namespace WebApp.Repositories
{
    public interface ICompanyRepository
    {
        Task InsertRevenueAsync(InsertRevenue revenueData);
        Task<IEnumerable<CompanyRevenue>> GetCompanyRevenuesAsync(int? companyId);
    }
}
