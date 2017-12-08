using GenesisVision.TradeIpfsStorage.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GenesisVision.TradeIpfsStorage.Models
{
    public class TradeContainer : ITradeContainer
    {
        public IList<IBaseTrade> Trades { get; set; }
        public IServer Server { get; set; }
        public IManager Manager { get; set; }
        public int SchemeVersion { get; set; }

        internal TradeContainer()
        {
        }
        
        public TradeContainer(IServer server, IManager manager, int schemeVersion)
        {
            Server = server;
            Manager = manager;
            SchemeVersion = schemeVersion;
            Trades = new List<IBaseTrade>();
        }

        public TradeContainer(IServer server, IManager manager, int schemeVersion, IEnumerable<IBaseTrade> trades)
        {
            Server = server;
            Manager = manager;
            SchemeVersion = schemeVersion;
            Trades = trades.ToList();
        }
    }
}
