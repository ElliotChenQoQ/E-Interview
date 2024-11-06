using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebApp.Services;

namespace WebApp.Handlers.Tests
{
    [TestClass()]
    public class SyncRevenueDataHandlerTests
    {
        private SyncRevenueDataHandler _handler;
        private Mock<ICompanyService> _companyServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _companyServiceMock = new Mock<ICompanyService>();
            _handler = new SyncRevenueDataHandler(_companyServiceMock.Object);
        }

        [TestMethod]
        public async Task Handle_CallsSyncRevenue()
        {
            // Arrange
            var command = new SyncRevenueDataCommand();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _companyServiceMock.Verify(s => s.SyncRevenueDataAsync(), Times.Once);
        }
    }
}