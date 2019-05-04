using System;
using Dyflissu.Shared.Configurations;

namespace Dyflissu.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigurationsManager.LoadConfiguration();
            var gameServer = new GameServer(
                ConfigurationsManager.ServerConfiguration,
                ConfigurationsManager.SharedConfiguration
            );

            gameServer.Run();
        }
    }
}