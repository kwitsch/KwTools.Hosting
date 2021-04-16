using KwTools.Hosting.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace KwTools.Hosting.TestExtension
{
    public class Configure : IAddServices
    {
        public Task AddServices(IServiceCollection hostBuilderCollection)
        {
            hostBuilderCollection.AddSingleton<ExternalTestClass>();
            return Task.CompletedTask;
        }
    }
}
