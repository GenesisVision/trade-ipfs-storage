using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using GenesisVision.TradeIpfsStorage.Interfaces;
using GenesisVision.TradeIpfsStorage.Models;

namespace TestTradeServer
{
    public class TestMetaTrader4Server
    {
        private readonly TradeContainer tradeContainer;
        private readonly List<IBaseTrade> trades;
        private readonly Timer timer;
        private readonly Random random;
        private readonly object lockObj;

        public TestMetaTrader4Server()
        {
            tradeContainer = new TradeContainer(
                new Server("127.0.0.1", "Test Server", ServerType.MetaTrader4),
                new Manager(Guid.NewGuid().ToString()), 1);
            trades = new List<IBaseTrade>();
            lockObj = new object();
            random = new Random();
            timer = new Timer(random.Next(1, 6) * 100);
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
            var trade = new Trade
            {
                TicketNum = random.Next(100000, 500000),
                Ticket = "",
                DateOpen = DateTime.Now.AddMinutes(-random.Next(200, 500)),
                DateClose = DateTime.Now.AddMinutes(random.Next(200, 500)),
                Direction = Direction.Buy,
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

        public TradeContainer GeTradeContainer()
        {
            lock (lockObj)
            {
                tradeContainer.Trades = trades.ToList();
                trades.Clear();
                timer.Interval = random.Next(1, 6) * 100;

                return tradeContainer;
            }
        }
    }
}
