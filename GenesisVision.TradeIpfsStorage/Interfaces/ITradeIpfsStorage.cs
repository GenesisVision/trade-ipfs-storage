using System.Threading;

namespace GenesisVision.TradeIpfsStorage.Interfaces
{
    public interface ITradeIpfsStorage
    {
        string UploadTrades(ITradeContainer tradeContainer, CancellationToken cancel = default(CancellationToken));

        ITradeContainer GetTrades(string hash, CancellationToken cancel = default(CancellationToken));
    }
}
