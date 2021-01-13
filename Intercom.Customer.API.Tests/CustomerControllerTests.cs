using Intercom.Customer.Contract;
using Intercom.Customer.Domain;
using InterCom.Customer.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Intercom.Customer.API.Tests
{
    public class CustomerControllerTests
    {
        Mock<ICustomerService> _customerServiceMock;
        Mock<ILogger<CustomerController>> _loggerMock;
        IOptions<AppSettings> _settings;

        [SetUp]
        public void Setup()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _loggerMock = new Mock<ILogger<CustomerController>>();
            _settings = Options.Create(new AppSettings
            {
                CustomerRecordFilePath = "https://test.url.com",
                IntercomCoords = new Location { Latitude = 53.339428, Longitude = -6.257664 },
                Distance = 100
            });
        }

        [Test]
        public async Task GetCustomers_InValidDistance()
        {
            var invalidDistance = 0;

            var controller = new CustomerController(_loggerMock.Object, _settings, _customerServiceMock.Object);
            var res = await controller.Get(invalidDistance, It.IsAny<string>());
            var objResult = res.Result as BadRequestResult;
            Assert.NotNull(res);
            Assert.True(objResult.StatusCode == (int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetCustomers_ServerError()
        {
            _customerServiceMock.Setup(repo => repo.GetNearByCustomers(It.IsAny<double>(), It.IsAny<string>()))
                .ThrowsAsync(new System.Exception("Server Error!"));

            var controller = new CustomerController(_loggerMock.Object, _settings, _customerServiceMock.Object);
            var res = await controller.Get(100, It.IsAny<string>());
            var objResult = res.Result as ObjectResult;
            Assert.NotNull(res);
            Assert.True(objResult.StatusCode == (int)HttpStatusCode.InternalServerError);
        }

        [Test]
        public async Task GetCustomers_OK()
        {
            var expectedValue = CustomerFactory.GetCustomers();
            _customerServiceMock.Setup(repo => repo.GetNearByCustomers(It.IsAny<double>(), It.IsAny<string>()))
                .ReturnsAsync(expectedValue);

            var controller = new CustomerController(_loggerMock.Object, _settings, _customerServiceMock.Object);
            var res = await controller.Get(100, It.IsAny<string>());
            var objResult = res.Result as ObjectResult;

            Assert.NotNull(res);
            Assert.True(objResult.StatusCode == (int)HttpStatusCode.OK);
            Assert.IsInstanceOf<List<Contract.Customer>>(objResult.Value);
            var resValue = objResult.Value as List<Contract.Customer>;
            CollectionAssert.AreEqual(expectedValue, resValue);
        }
    }
}