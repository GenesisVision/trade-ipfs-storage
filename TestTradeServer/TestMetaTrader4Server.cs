using GenesisVision.TradeIpfsStorage.Interfaces.Trades;
using GenesisVision.TradeIpfsStorage.Models.Trades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace TestTradeServer
{
    public class TestMetaTrader4Server
    {
        private readonly List<IMetaTraderOrder> trades;
        private readonly Timer timer;
        private readonly Random random;
        private readonly object lockObj;

        public TestMetaTrader4Server()
        {
            trades = new List<IMetaTraderOrder>();
            lockObj = new object();
            random = new Random();
            timer = new Timer(random.Next(100, 300));
            timer.Elapsed += (sender, args) => CreateTrade();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        private void CreateTrade()
        {
            var trade = new MetaTraderOrder
                        {
                            Ticket = random.Next(100000, 500000),
                            Login = random.Next(100, 104),
                            DateOpen = DateTime.Now.AddMinutes(-random.Next(200, 500)),
                            DateClose = DateTime.Now.AddMinutes(random.Next(200, 500)),
                            Direction = random.Next(0, 10) > 5 ? Direction.Buy : Direction.Sell,
                            PriceOpen = 1 - (random.Next(0, 100) * 0.001m),
                            PriceClose = 1 + (random.Next(0, 100) * 0.001m),
                            Profit = random.Next(100, 500),
                            Symbol = "TEST",
                            Volume = random.Next(1, 10)
                        };
            lock (lockObj)
            {
                trades.Add(trade);
            }
        }

        public List<IMetaTraderOrder> GetTrades()
        {
            lock (lockObj)
            {
                var tmp = trades.ToList();
                trades.Clear();
                timer.Interval = random.Next(100, 300);

                return tmp;
            }
        }
    }
}
