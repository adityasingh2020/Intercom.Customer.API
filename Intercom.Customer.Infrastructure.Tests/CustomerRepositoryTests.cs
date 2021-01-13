using Intercom.Customer.Contract;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intercom.Customer.Infrastructure.Tests
{
    public class CustomerRepositoryTests
    {
        Mock<ICustomerClient> _customerClientMock;
        IOptions<AppSettings> _settings;

        [SetUp]
        public void Setup()
        {
            _customerClientMock = new Mock<ICustomerClient>();
            _settings = Options.Create(SettingsFactory.GetAppSettings());
        }

        [Test]
        public void CustomerClient_response_500()
        {
            _customerClientMock.Setup(client => client.GetCustomerDeatils(It.IsAny<string>()))
                .ThrowsAsync(new HttpRequestException());

            var sut = new CustomerRepository(
                _settings,
                _customerClientMock.Object);

            Assert.ThrowsAsync<HttpRequestException>(() => sut.GetNearByCustomers(_settings.Value.Distance, _settings.Value.CustomerRecordFilePath));
        }

        [Test]
        public async Task CustomerRepositoryTests_1()
        {
            var customerInfos = new List<CustomerInfo>
            {
                new CustomerInfo { UserId = 1, Name = "test", Latitude = _settings.Value.IntercomCoords.Latitude, Longitude = _settings.Value.IntercomCoords.Longitude}
            };
            _customerClientMock.Setup(client => client.GetCustomerDeatils(It.IsAny<string>()))
                .ReturnsAsync(customerInfos);

            var sut = new CustomerRepository(
                _settings,
                _customerClientMock.Object);

            var res = await sut.GetNearByCustomers(0.1, _settings.Value.CustomerRecordFilePath);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(customerInfos[0].UserId, res[0].UserId);
        }

        [Test]
        [TestCase(10 ,1)]
        [TestCase(20, 2)]
        [TestCase(30, 3)]
        [TestCase(40, 4)]
        [TestCase(100, 5)]
        [TestCase(200, 6)]
        [TestCase(300, 7)]
        public async Task CustomerRepositoryTests_MultipleTests(double distance, int expectedNumOfCustomers)
        {
            var customerInfos = CustomerFactory.GetCustomerInfos();
            _customerClientMock.Setup(client => client.GetCustomerDeatils(It.IsAny<string>()))
                .ReturnsAsync(customerInfos);

            var sut = new CustomerRepository(
                _settings,
                _customerClientMock.Object);

            var res = await sut.GetNearByCustomers(distance, _settings.Value.CustomerRecordFilePath);
            Assert.AreEqual(expectedNumOfCustomers, res.Count);
        }
    }
}