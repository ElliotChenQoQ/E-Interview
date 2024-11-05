// CompaniesController.cs
using MediatR;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Handlers
{
    public class GetCompanyRevenuesHandler : IRequestHandler<GetCompanyRevenuesQuery, IEnumerable<CompanyRevenue>>
    {
        private readonly ICompanyService _companyService;

        public GetCompanyRevenuesHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IEnumerable<CompanyRevenue>> Handle(GetCompanyRevenuesQuery request, CancellationToken cancellationToken)
        {
            return await _companyService.GetCompanyRevenuesAsync(request.CompanyId);
        }
    }
}
