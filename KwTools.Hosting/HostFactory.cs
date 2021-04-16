using KwTools.Hosting.Interfaces;
using KwTools.Hosting.Util;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace KwTools.Hosting
{
    /// <summary>
    /// Factory for configured Host object production
    /// </summary>
    public class HostFactory
    {
        #region singelton
        private static readonly Lazy<HostFactory> _Instance;
        /// <summary>
        /// ConfigFactory Instanz
        /// </summary>
        public static HostFactory Instance => _Instance.Value;
        static HostFactory()
        {
            _Instance = new Lazy<HostFactory>(() => new HostFactory());
        }
        private HostFactory()
        {

        }
        #endregion

        /// <summary>
        /// Run a configured Host instance 
        /// </summary>
        /// <returns>async Task</returns>
        public async Task RunHostAsync()
        {
            var host = await GetHostAsync();
            await host.RunAsync();
        }

        /// <summary>
        /// Get a configured Host instance 
        /// </summary>
        /// <returns>async Task</returns>
        public async Task<IHost> GetHostAsync()
        {
            var domainActivator = new DomainActivator();
            var builder = await GetHostBuilderAsync(domainActivator);
            var res = builder.Build();
            await domainActivator.GetInstances<IConfigureHost>()
                                 .ConfAllAsync(res);
            return res;
        }

        /// <summary>
        /// Get a configured HostBuilder instance 
        /// </summary>
        /// <returns>async Task</returns>
        public async Task<IHostBuilder> GetHostBuilderAsync()
            => await GetHostBuilderAsync(new DomainActivator());


        #region internal
        private async Task<IHostBuilder> GetHostBuilderAsync(DomainActivator domainActivator)
        {
            var res = new HostBuilder();
            res.ConfigureServices(async builderCollection =>
            {
                await domainActivator.GetInstances<IAddServices>()
                                     .AddAllAsync(builderCollection);
            });
            await domainActivator.GetInstances<IConfigureBuilder>()
                                 .ConfAllAsync(res);

            return res;
        }
        #endregion

    }
}
