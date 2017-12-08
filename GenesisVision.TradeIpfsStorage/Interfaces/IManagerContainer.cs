using System;
using System.Collections.Generic;
using System.Text;

namespace GenesisVision.TradeIpfsStorage.Interfaces
{
    public interface IManagerContainer
    {
        ITradeContainer TradeContainer { get; }
    }
}
