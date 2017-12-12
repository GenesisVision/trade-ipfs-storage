using GenesisVision.TradeIpfsStorage;
using System;
using System.Threading;

namespace TestTradeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var ipfsStorage = new TradeIpfsStorage(
                    "http://localhost:5001", 
                    "http://localhost:8545",
                    "",
                    "",
                    "",
                    "");

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
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}
