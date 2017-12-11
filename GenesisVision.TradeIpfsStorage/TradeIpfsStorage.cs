using GenesisVision.TradeIpfsStorage.Interfaces;
using Ipfs.Api;
using Nethereum.Geth;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Nethereum.Contracts;

namespace GenesisVision.TradeIpfsStorage
{
    public class TradeIpfsStorage : ITradeIpfsStorage
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IpfsClient ipfs;
        private readonly ContractHelper contractHelper;
        
        private readonly string serverId;
        private Dictionary<string, string> managersIpfsHistory;
        
        public TradeIpfsStorage(string ipfsHost, string gethHost, string contractAddress, string serverId)
        {
            logger.Info($"Initialize GenesisVision.TradeIpfsStorage...");
            logger.Info($"IPFS host: {ipfsHost}");
            logger.Info($"Geth host: {gethHost}");
            logger.Info($"Contract Address: {contractAddress}");
            logger.Info($"Server id: {serverId}");

            ipfs = new IpfsClient(ipfsHost);
            contractHelper = new ContractHelper(gethHost, contractAddress);
            this.serverId = serverId;
            managersIpfsHistory = new Dictionary<string, string>();
            
            var managers = GetServerManagers();
            var managersData = contractHelper.GetManagers(managers);
        }

        private IEnumerable<string> GetServerManagers()
        {
            // todo: getting managers from GV server

            //var managers = GenesisVision.Core.GetManagers(serverId);

            var managers = new List<string>
                           {
                               "8E916E5F-C12C-488A-B298-438CB4F51A75",
                               "AE4DB832-2D99-4971-A74E-A4B2D6FEB3C1",
                               "CD16E803-9BCC-41EB-AE78-6E559ACB95FF"
                           };
            logger.Info($"Loaded {managers.Count} managers");

            return managers;
        }

        private void GetManagersIpfsHash(IEnumerable<string> managerIds)
        {
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
                new JsonConverters.TradeConverter(),
                new JsonConverters.ManagerConverter());

            logger.Info($"Hash {hash}. Load {tradeContainer.Trades.Count} trades");

            return tradeContainer;
        }
    }
}
