using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace KwTools.Hosting.Interfaces
{
    /// <summary>
    /// Interface for class based HostBuilder configuration
    /// </summary>
    public interface IConfigureBuilder
    {
        /// <summary>
        /// Configure the HostBuilder
        /// </summary>
        /// <param name="hostBuilder">HostBuilder</param>
        /// <returns>Task</returns>
        Task ConfigureBuilder(IHostBuilder hostBuilder);
    }
}
