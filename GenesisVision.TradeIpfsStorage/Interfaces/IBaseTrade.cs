using System;

namespace GenesisVision.TradeIpfsStorage.Interfaces
{
    public enum Direction
    {
        Buy = 0,
        Sell = 1
    }

    public interface IBaseTrade
    {
        long TicketNum { get; set; }
        string Ticket { get; set; }
        string Symbol { get; set; }
        decimal PriceOpen { get; set; }
        decimal PriceClose { get; set; }
        decimal Profit { get; set; }
        decimal Volume { get; set; }
        DateTime DateOpen { get; set; }
        DateTime DateClose { get; set; }
        Direction Direction { get; set; }
    }
}
