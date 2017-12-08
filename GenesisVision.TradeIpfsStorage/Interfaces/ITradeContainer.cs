using System.Collections.Generic;

namespace GenesisVision.TradeIpfsStorage.Interfaces
{
    public interface ITradeContainer : ISchemeVersion
    {
        IServer Server { get; set; }
        IList<IBaseTrade> Trades { get; set; }
    }
}