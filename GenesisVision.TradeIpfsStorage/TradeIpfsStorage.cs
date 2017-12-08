using GenesisVision.TradeIpfsStorage.Interfaces;
using Ipfs.Api;
using Newtonsoft.Json;
using NLog;
using System;
using System.Linq;
using System.Threading;

namespace GenesisVision.TradeIpfsStorage
{
    public class TradeIpfsStorage : ITradeIpfsStorage
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IpfsClient ipfs;

        public TradeIpfsStorage(string host)
        {
            ipfs = new IpfsClient(host);
        }

        public TradeIpfsStorage()
        {
            ipfs = new IpfsClient();
        }

        public string UploadTrades(ITradeContainer tradeContainer, CancellationToken cancel = default(CancellationToken))
        {
            if (tradeContainer.Trades == null || tradeContainer.Server == null)
                throw new ArgumentNullException();

            logger.Info($"Upload trades. Server: {tradeContainer.Server.Name}, " +
                        $"SchemeVersion: {tradeContainer.SchemeVersion}, " +
                        $"Trades: {tradeContainer.Trades.Count}");
            if (!tradeContainer.Trades.Any())
                return null;

            var json = JsonConvert.SerializeObject(tradeContainer);
            var res = ipfs.FileSystem.AddTextAsync(json, cancel).Result;

            logger.Info($"Uploaded successfully. Hash {res.Hash}");

            return res.Hash;
        }

        public ITradeContainer GetTrades(string hash, CancellationToken cancel = default(CancellationToken))
        {
            logger.Info($"Getting trades by hash {hash}...");

            var json = ipfs.FileSystem.ReadAllTextAsync(hash, cancel).Result;
            var tradeContainer = JsonConvert.DeserializeObject<ITradeContainer>(json,
                new JsonConverters.TradeContainerConverter(),
                new JsonConverters.ServerConverter(),
                new JsonConverters.TradeConverter());

            logger.Info($"Hash {hash}. Load {tradeContainer.Trades.Count} trades");

            return tradeContainer;
        }
    }
}
