// CompaniesController.cs
using MediatR;
using WebApp.Services;

namespace WebApp.Handlers
{
    public class SyncRevenueDataHandler : IRequestHandler<SyncRevenueDataCommand, Unit>
    {
        private readonly ICompanyService _companyService;

        public SyncRevenueDataHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<Unit> Handle(SyncRevenueDataCommand request, CancellationToken cancellationToken)
        {
            await _companyService.SyncRevenueDataAsync();

            return Unit.Value;
        }
    }
}
