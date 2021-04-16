using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace KwTools.Hosting.Interfaces
{
    /// <summary>
    /// Interface for class based Host configuration
    /// </summary>
    public interface IConfigureHost
    {
        /// <summary>
        /// Configure the Host
        /// </summary>
        /// <param name="host">Host</param>
        /// <returns>Task</returns>
        Task ConfigureHost(IHost host);
    }
}
