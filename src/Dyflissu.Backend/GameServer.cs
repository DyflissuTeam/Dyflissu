using System;
using System.Diagnostics;
using System.Threading;
using Dyflissu.Shared;
using Dyflissu.Shared.Configurations;
using Lidgren.Network;
using NLog;

namespace Dyflissu.Backend
{
    public class GameServer
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ServerConfiguration _serverConfiguration;
        private readonly SharedConfiguration _sharedConfiguration;
        private NetServer _server;
        private bool _isRunning;
        private GameWorld world;

        public GameServer(ServerConfiguration serverConfiguration, SharedConfiguration sharedConfiguration)
        {
            _serverConfiguration = serverConfiguration;
            _sharedConfiguration = sharedConfiguration;

            world = new GameWorld();
        }

        public void Run()
        {
            if (_isRunning)
            {
                throw new InvalidOperationException("Server is already running.");
            }

            SetupNetworking();

            _server.Start();
            _isRunning = true;

            ServerLoop();
        }

        private void ServerLoop()
        {
            DateTime currentUpdateTime = DateTime.Now;

            while (_isRunning)
            {
                DateTime previousUpdateTime = currentUpdateTime;
                currentUpdateTime = DateTime.Now;

                float delta = (float) (currentUpdateTime - previousUpdateTime).Ticks / TimeSpan.TicksPerSecond;

                LoopIteration(delta);
                float updateTime = (float) (currentUpdateTime - DateTime.Now).Ticks / TimeSpan.TicksPerSecond;

                var missingDelay = (int) ((_serverConfiguration.MinimalUpdateDelta - updateTime) * 1000);
                if (missingDelay > 0)
                {
                    Thread.Sleep(missingDelay);
                }
                else
                {
                    _logger.Warn($"Server loop iteration takes more time than suppose ({delta}s).");
                }
            }
        }

        private void LoopIteration(float delta)
        {
            _logger.Info($"Delta: {delta}");
            world.Update(delta);
        }

        private void SetupNetworking()
        {
            var netPeerConfiguration = new NetPeerConfiguration(_sharedConfiguration.Networking.Name)
            {
                Port = _serverConfiguration.Port,
                AutoExpandMTU = true
            };

            _server = new NetServer(netPeerConfiguration);
        }
    }
}