using GenesisVision.TradeIpfsStorage;
using System;

namespace TestTradeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var ipfsStorage = new TradeIpfsStorage("http://localhost:5001");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}
