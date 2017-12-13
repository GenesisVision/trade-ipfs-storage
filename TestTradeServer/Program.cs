using GenesisVision.TradeIpfsStorage;
using NLog;
using System;
using System.Configuration;
using System.Threading;

namespace TestTradeServer
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                var ipfsHost = ConfigurationManager.AppSettings["ipfsHost"];
                var gethHost = ConfigurationManager.AppSettings["gethHost"];
                var contractAddress = ConfigurationManager.AppSettings["contractAddress"];
                var brokerAddress = ConfigurationManager.AppSettings["brokerAddress"];
                var brokerPassword = ConfigurationManager.AppSettings["brokerPassword"];
                var serverId = ConfigurationManager.AppSettings["serverId"];

                var ipfsStorage = new TradeIpfsStorage(ipfsHost, gethHost, contractAddress, brokerAddress, brokerPassword, serverId);

                var testServer = new TestMetaTrader4Server();
                testServer.Start();

                while (true)
                {
                    var trades = testServer.GetTrades();
                    ipfsStorage.StoreNewTrades(trades);
                    
                    Thread.Sleep(10 * 1000);
                }
            }
            catch (Exception e)
            {
                logger.Fatal($"Application stopped: {e.Message} {Environment.NewLine}{e.StackTrace}");
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
    }
}
