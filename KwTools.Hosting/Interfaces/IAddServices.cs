using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace KwTools.Hosting.Interfaces
{
    /// <summary>
    /// Interface for class based ServiceCollection configuration
    /// </summary>
    public interface IAddServices
    {
        /// <summary>
        /// Add services to HostBuilder ServiceCollection
        /// </summary>
        /// <param name="hostBuilderCollection">HostBuilder ServiceCollection during configuration</param>
        /// <returns>Task</returns>
        Task AddServices(IServiceCollection hostBuilderCollection);
    }
}
