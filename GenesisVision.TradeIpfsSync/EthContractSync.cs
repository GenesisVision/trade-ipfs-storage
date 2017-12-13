using GenesisVision.PlatformContract;
using Ipfs.Api;
using Nethereum.Geth;
using Nethereum.RPC.Eth.DTOs;
using System.Collections.Generic;
using Nethereum.Web3;
using NLog;

namespace GenesisVision.TradeIpfsSync
{
    public class EthContractSync
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly PlatformContractService platformContractService;
        private readonly IpfsClient ipfs;
        private readonly Web3 web3;
        
        private readonly int lastEventsCount;
        private readonly HashSet<string> loadedTradeHashes;

        public EthContractSync(string gethHost, string contractAddress, string ipfsHost, int lastEventsCount = 100)
        {
            logger.Info($"Initialize GenesisVision.TradeIpfsSync...");

            logger.Info($"IPFS host: {ipfsHost}");
            logger.Info($"Geth host: {gethHost}");
            logger.Info($"Contract Address: {contractAddress}");
            logger.Info($"Last events count: {lastEventsCount}");

            web3 = new Web3Geth(gethHost);
            platformContractService = new PlatformContractService(web3, contractAddress);
            ipfs = new IpfsClient(ipfsHost);

            this.lastEventsCount = lastEventsCount;
            loadedTradeHashes = new HashSet<string>();
        }

        public void SyncNewTrades(long? fromBlock = null)
        {
            var blockTo = long.Parse(web3.Eth.Blocks.GetBlockNumber.SendRequestAsync().Result.Value.ToString());
            var blockFrom = fromBlock ?? blockTo - lastEventsCount;

            logger.Debug($"Start syncing {blockFrom}-{blockTo}");

            var filter = platformContractService.GetEventManagerUpdated()
                                                .CreateFilterBlockRangeAsync(new BlockParameter((ulong)blockFrom), new BlockParameter((ulong)blockTo))
                                                .Result;
            var eventLogs = platformContractService.GetEventManagerUpdated()
                                                   .GetAllChanges<ManagerUpdatedEventDTO>(filter)
                                                   .Result;

            logger.Debug($"Find {eventLogs.Count} events");

            var managers = new HashSet<string>();
            foreach (var e in eventLogs)
            {
                managers.Add(e.Event.ManagerId);
            }

            foreach (var manager in managers)
            {
                var ipfsHash = platformContractService.GetManagerHistoryIpfsHashAsyncCall(manager).Result;
                if (loadedTradeHashes.Add(ipfsHash))
                {
                    var data = ipfs.FileSystem.ReadAllTextAsync(ipfsHash).Result;

                    logger.Info($"Sync trades. Manager {manager}, IPFS hash {ipfsHash}");
                }
            }
        }
    }
}
