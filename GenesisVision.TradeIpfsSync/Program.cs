using NLog;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace GenesisVision.TradeIpfsSync
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                var gethHost = ConfigurationManager.AppSettings["gethHost"];
                var contractAddress = ConfigurationManager.AppSettings["contractAddress"];
                var ipfsHost = ConfigurationManager.AppSettings["ipfsHost"];
                var lastEventsCount = ConfigurationManager.AppSettings["lastEventsCount"];

                var sync = int.TryParse(lastEventsCount, out var count)
                    ? new EthContractSync(gethHost, contractAddress, ipfsHost, count)
                    : new EthContractSync(gethHost, contractAddress, ipfsHost);

                if (args != null && args.Length > 0 && long.TryParse(args.First(), out var lastBlock))
                {
                    sync.SyncNewTrades(lastBlock);
                }

                while (true)
                {
                    sync.SyncNewTrades();

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
