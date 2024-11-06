using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using WebApp.Models;
using AutoMapper;
using MediatR;
using WebApp.Controllers;
using WebApp.Services;
using Moq;

namespace WebApp.Handlers.Tests
{
    [TestClass()]
    public class GetCompanyRevenuesHandlerTests
    {
        private GetCompanyRevenuesHandler _handler;
        private Mock<ICompanyService> _companyServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _companyServiceMock = new Mock<ICompanyService>();
            _handler = new GetCompanyRevenuesHandler(_companyServiceMock.Object);
        }

        [TestMethod]
        public async Task Handle_ValidId_ReturnsRevenues()
        {
            // Arrange
            int companyId = 1;
            var revenueList = new List<CompanyRevenue> { new CompanyRevenue() };
            _companyServiceMock.Setup(s => s.GetCompanyRevenuesAsync(companyId)).ReturnsAsync(revenueList);

            var query = new GetCompanyRevenuesQuery(companyId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result?.Count() > 0);
        }

        [TestMethod]
        public async Task Handle_InvalidId_ReturnsEmptyList()
        {
            // Arrange
            int companyId = 999;
            _companyServiceMock.Setup(s => s.GetCompanyRevenuesAsync(companyId)).ReturnsAsync(new List<CompanyRevenue>());

            var query = new GetCompanyRevenuesQuery(companyId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.AreEqual(0, result?.Count());
        }
    }
}