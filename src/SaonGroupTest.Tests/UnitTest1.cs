using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SaonGroupTest.Client.Services;
using SaonGroupTest.Tests;

namespace SaonGrouptest.Tests
{
    [TestFixture]

    public class Tests : Testing
    {
        [OneTimeSetUp]
        public async Task Setup()
        {
            TestContext.Progress.WriteLine("One time setup");
            RunBeforeAnyTests();
        }


        [Test]
        public async Task RegionsTests()
        {
            var covidMockedService = new Mock<ICovidRapiApiService>();
        }
    }
}