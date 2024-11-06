using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Repositories;

namespace Tests
{
    [TestClass()]
    public class CompanyServiceTests
    {
        private CompanyService _service;
        private Mock<ICompanyRepository> _repositoryMock;
        private Mock<IHttpClientFactory> _httpClientFactoryMock;
        private Mock<IMapper> _mapperMock;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<ICompanyRepository>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _mapperMock = new Mock<IMapper>();
            _service = new CompanyService(_repositoryMock.Object, _mapperMock.Object, _httpClientFactoryMock.Object);
        }

        [TestMethod]
        public async Task FetchAndParseCsvData_CallsInsertRevenue()
        {
            // Arrange: 模擬 CSV 資料
            string csvContent =
                "出表日期,資料年月,公司代號,公司名稱,產業別,營業收入-當月營收,營業收入-上月營收,營業收入-去年當月營收,營業收入-上月比較增減(%),營業收入-去年同月增減(%),累計營業收入-當月累計營收,累計營業收入-去年累計營收,累計營業收入-前期比較增減(%),備註\n" +
                "1131017,11309,1101,台泥,水泥工業,13325249,13004435,8735157,2.47,52.55,105591346,80942003,30.45,2024/3/6起併入OYAK及Cimpor，以致營收較去年同期增加。\n" +
                "1131017,11309,1102,亞泥,水泥工業,6496635,6734055,6691905,-3.53,-2.92,55437813,60239086,-7.97,-\n";

            // 設置 HttpClient 模擬回應
            var httpClientMockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            httpClientMockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(csvContent)
                });

            var httpClient = new HttpClient(httpClientMockHandler.Object);
            _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Mapper 模擬 
            _mapperMock
                .Setup(mapper => mapper.Map<InsertRevenue>(It.IsAny<CsvCompanyRevenue>()))
                .Returns((CsvCompanyRevenue source) => new InsertRevenue ());

            // InsertRevenueAsync 模擬 
            _repositoryMock.Setup(repo => repo.InsertRevenueAsync(It.IsAny<InsertRevenue>()))
           .Returns(Task.CompletedTask);

            // Act: 執行測試方法
            await _service.SyncRevenueDataAsync();

            // Assert: 驗證 InsertRevenueAsync 次數呼叫是否正確
            _repositoryMock.Verify(repo => repo.InsertRevenueAsync(It.IsAny<InsertRevenue>()), Times.Exactly(2));
        }
    }
}