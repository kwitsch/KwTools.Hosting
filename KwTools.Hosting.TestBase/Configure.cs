using KwTools.Hosting.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace KwTools.Hosting.TestBase
{
    public class Configure : IAddServices
    {
        public Task AddServices(IServiceCollection hostBuilderCollection)
        {
            hostBuilderCollection.AddHostedService<TestService>();
            return Task.CompletedTask;
        }
    }
}
