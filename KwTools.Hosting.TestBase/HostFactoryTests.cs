using KwTools.Hosting.TestExtension;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace KwTools.Hosting.TestBase
{
    public class HostFactoryTests

    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task HostBuilderTest()
        {
            var builder = await HostFactory.Instance.GetHostBuilderAsync();
            var host = builder.Build();
            Assert.IsNotNull(host.Services.GetService<ExternalTestClass>());
        }

        [Test]
        public async Task HostTest()
        {
            var host = await HostFactory.Instance.GetHostAsync();
            Assert.IsNotNull(host.Services.GetService<ExternalTestClass>());
        }

        [Test]
        public async Task RunTest()
        {
            await HostFactory.Instance.RunHostAsync();
            Assert.Pass();
        }
    }
}