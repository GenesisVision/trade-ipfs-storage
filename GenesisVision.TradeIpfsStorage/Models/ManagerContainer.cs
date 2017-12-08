using GenesisVision.TradeIpfsStorage.Interfaces;

namespace GenesisVision.TradeIpfsStorage.Models
{
    public class ManagerContainer : IManagerContainer
    {
        private ITradeContainer tradeContainer;

        public ManagerContainer(ITradeContainer tradeContainer)
        {
            this.tradeContainer = tradeContainer;
        }
        public ITradeContainer TradeContainer => tradeContainer;
    }
}
