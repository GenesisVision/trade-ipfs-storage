using System.Collections.Generic;
using GenesisVision.TradeIpfsStorage.Interfaces;
using Nethereum.Contracts;
using Nethereum.Geth;

namespace GenesisVision.TradeIpfsSync
{
    public class EthContractSync
    {
        private readonly Web3Geth web3;
        private readonly Event managerUpdatedEvent;
        private readonly ITradeIpfsStorage storage;

        private long lastBlock;
        private HashSet<string> loadedTrades;

        private const int lastCount = 100;

        public EthContractSync(string gethAddress, string contractAddress, string abi, string ipfsHost)
        {
            web3 = new Web3Geth(gethAddress);
            var contract = web3.Eth.GetContract(abi, contractAddress);
            managerUpdatedEvent = contract.GetEvent("ManagerUpdated");
            loadedTrades = new HashSet<string>();
            lastBlock = long.Parse(web3.Eth.Blocks.GetBlockNumber.SendRequestAsync().Result.ToString());
        }

        public IEnumerable<string> GetNewTradeHashes(long? fromBlock = null)
        {
            return null;
        }
    }
}
