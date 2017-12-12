using System;
using GenesisVision.TradeIpfsStorage.Interfaces.Trades;

namespace GenesisVision.TradeIpfsStorage.Models.Trades
{
    public class MetaTraderOrder : IMetaTraderOrder
    {
        public long Ticket { get; set; }
        public long Login { get; set; }
        public string Symbol { get; set; }
        public decimal PriceOpen { get; set; }
        public decimal PriceClose { get; set; }
        public decimal Profit { get; set; }
        public decimal Volume { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public Direction Direction { get; set; }
    }
}
