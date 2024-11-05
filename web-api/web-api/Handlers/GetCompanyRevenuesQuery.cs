// CompaniesController.cs
using MediatR;
using WebApp.Models;

namespace WebApp.Handlers
{
    public class GetCompanyRevenuesQuery : IRequest<IEnumerable<CompanyRevenue>>
    {
        public int? CompanyId { get; }

        public GetCompanyRevenuesQuery(int? companyId)
        {
            CompanyId = companyId;
        }
    }
}
