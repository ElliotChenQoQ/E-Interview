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
            var csvUrl = "https://mopsfin.twse.com.tw/opendata/t187ap05_L.csv";
            var csvData = await _companyService.GetCsvDataAsync(csvUrl);
            var csvRecords = _companyService.ParseCsvData(csvData);
            var records = _companyService.ConvertToInsertRevenue(csvRecords);
            await _companyService.InsertRevenuesAsync(records);

            return Unit.Value;
        }
    }
}
