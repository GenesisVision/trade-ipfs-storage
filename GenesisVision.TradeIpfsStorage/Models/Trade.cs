using System;
using GenesisVision.TradeIpfsStorage.Interfaces;

namespace GenesisVision.TradeIpfsStorage.Models
{
    public class Trade : IBaseTrade
    {
        public long TicketNum { get; set; }
        public string Ticket { get; set; }
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
