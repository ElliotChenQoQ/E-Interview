using AutoMapper;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Services;
using System;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;

    public CompanyService(ICompanyRepository repository, IMapper mapper, IHttpClientFactory httpClientFactory)
    {
        _repository = repository;
        _mapper = mapper;
        _httpClientFactory = httpClientFactory;
    }

    public async Task SyncRevenueDataAsync()
    {
        string csvUrl = "https://mopsfin.twse.com.tw/opendata/t187ap05_L.csv";
        string csvData = await GetCsvDataAsync(csvUrl);
        List<CsvCompanyRevenue> csvRecords = ParseCsvData(csvData);
        List<InsertRevenue> revenueRecords = ConvertToInsertRevenue(csvRecords);
        await InsertRevenuesAsync(revenueRecords);
    }

    public async Task<string> GetCsvDataAsync(string url) =>
        await _httpClientFactory.CreateClient().GetStringAsync(url);

    public List<CsvCompanyRevenue> ParseCsvData(string csvData)
    {
        using var reader = new StringReader(csvData);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        });
        csv.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = new[] { "yyyyMMdd" };
        return csv.GetRecords<CsvCompanyRevenue>().ToList();
    }

    public List<InsertRevenue> ConvertToInsertRevenue(List<CsvCompanyRevenue> csvRecords) =>
        csvRecords.ConvertAll(x => _mapper.Map<InsertRevenue>(x));

    public async Task InsertRevenuesAsync(List<InsertRevenue> records)
    {
        foreach (var record in records)
        {
            await _repository.InsertRevenueAsync(record);
        }
    }

    public async Task<IEnumerable<CompanyRevenue>> GetCompanyRevenuesAsync(int? companyId) =>
            await _repository.GetCompanyRevenuesAsync(companyId);
}
