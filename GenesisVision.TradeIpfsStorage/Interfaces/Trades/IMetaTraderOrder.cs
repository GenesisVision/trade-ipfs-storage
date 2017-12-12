using System;

namespace GenesisVision.TradeIpfsStorage.Interfaces.Trades
{
    public enum Direction
    {
        Buy = 0,
        Sell = 1
    }

    public interface IMetaTraderOrder
    {
        long Login { get; set; }
        long Ticket { get; set; }
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
