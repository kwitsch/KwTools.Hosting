using KwTools.Hosting.TestExtension;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace KwTools.Hosting.TestBase
{
    class TestService : BackgroundService
    {
        private readonly IHostApplicationLifetime _HostApplicationLifetime;
        private readonly ExternalTestClass _ExternalTestClass;
        public TestService(IHostApplicationLifetime hostApplicationLifetime, ExternalTestClass externalTestClass)
        {
            _HostApplicationLifetime = hostApplicationLifetime;
            _ExternalTestClass = externalTestClass;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ExternalTestClass.WriteTypeName();
            _HostApplicationLifetime.StopApplication();
            return Task.CompletedTask;
        }
    }
}
