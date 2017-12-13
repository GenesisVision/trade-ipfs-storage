using System.Linq;
using System.Threading;

namespace GenesisVision.TradeIpfsSync
{
    class Program
    {
        static void Main(string[] args)
        {
            var sync = new EthContractSync("http://localhost:8545",
                "",
                "http://localhost:5001");

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
    }
}
