using Ipfs.Api;
using NLog;

namespace GenesisVision.TradeIpfsStorage.Services
{
    internal class IpfsService
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IpfsClient ipfs;

        public IpfsService(string ipfsHost)
        {
            ipfs = new IpfsClient(ipfsHost);
        }

        public string UploadTrades(string text, string managerId)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            logger.Debug($"Upload trades to IPFS. Manager id: {managerId}");
            
            var res = ipfs.FileSystem.AddTextAsync(text).Result;

            logger.Info($"Trades to IPFS uploaded. Manager id: {managerId}, hash {res.Hash}");

            return res.Hash;
        }

        public string GetTextDataFromIpfs(string hash)
        {
            logger.Debug($"Getting data from IPFS by hash {hash}...");

            var data = ipfs.FileSystem.ReadAllTextAsync(hash).Result;
            return data;
        }
    }
}
