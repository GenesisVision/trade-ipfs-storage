﻿using GenesisVision.TradeIpfsStorage;
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
                var ipfsStorage = new TradeIpfsStorage("http://localhost:5001");

                var testServer = new TestMetaTrader4Server();
                testServer.Start();

                while (true)
                {
                    var tradeContainer = testServer.GeTradeContainer();
                    var hash = ipfsStorage.UploadTrades(tradeContainer);

                    if (!string.IsNullOrEmpty(hash))
                    {
                        var data = ipfsStorage.GetTrades(hash);
                    }

                    Thread.Sleep(5 * 1000);
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
