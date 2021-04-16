using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KwTools.Hosting.Interfaces
{
    /// <summary>
    /// ExtensionMethodes for IAddServices, IConfigureBuilder and IConfigureHost
    /// </summary>
    public static class InterfaceExtensions
    {
        /// <summary>
        /// Enumerates an IAddServices collection and executes AddServices for all
        /// </summary>
        /// <param name="self">IAddServices collection</param>
        /// <param name="serviceCollection">HostBuilder ServiceCollection</param>
        /// <returns>async Task</returns>
        public static async Task AddAllAsync(this IEnumerable<IAddServices> self, IServiceCollection serviceCollection)
        {
            var taskList = new List<Task>();
            foreach (var addition in self)
            {
                taskList.Add(addition.AddServices(serviceCollection));
            }
            await Task.WhenAll(taskList);
        }

        /// <summary>
        /// Enumerates an IConfigureBuilder collection and executes ConfigureBuilder for all
        /// </summary>
        /// <param name="self">IConfigureBuilder collection</param>
        /// <param name="hostBuilder">HostBuilder</param>
        /// <returns>async Task</returns>
        public static async Task ConfAllAsync(this IEnumerable<IConfigureBuilder> self, IHostBuilder hostBuilder)
        {
            var taskList = new List<Task>();
            foreach (var conf in self)
            {
                taskList.Add(conf.ConfigureBuilder(hostBuilder));
            }
            await Task.WhenAll(taskList);
        }

        /// <summary>
        /// Enumerates an IConfigureHost collection and executes ConfigureHost for all
        /// </summary>
        /// <param name="self">IConfigureHost collection</param>
        /// <param name="host">Host</param>
        /// <returns>async Task</returns>
        public static async Task ConfAllAsync(this IEnumerable<IConfigureHost> self, IHost host)
        {
            var taskList = new List<Task>();
            foreach (var conf in self)
            {
                taskList.Add(conf.ConfigureHost(host));
            }
            await Task.WhenAll(taskList);
        }
    }
}
