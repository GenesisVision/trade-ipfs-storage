using System.Collections.Generic;
using GenesisVision.TradeIpfsStorage.Interfaces.Trades;

namespace GenesisVision.TradeIpfsStorage.Interfaces
{
    public interface ITradeIpfsStorage
    {
        void StoreNewTrades(IEnumerable<IMetaTraderOrder> trades);
    }
}
