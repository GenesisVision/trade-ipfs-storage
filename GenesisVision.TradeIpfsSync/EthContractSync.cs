using System.Collections.Generic;
using Nethereum.Contracts;
using Nethereum.Geth;

namespace GenesisVision.TradeIpfsSync
{
    public class EthContractSync
    {
        private readonly Web3Geth web3;
        private readonly Event tradeEvent;

        private long lastBlock;
        private HashSet<string> loadedTrades;

        public EthContractSync(string gethAddress, string contractAddress)
        {
            web3 = new Web3Geth(gethAddress);
            loadedTrades = new HashSet<string>();
            lastBlock = long.Parse(web3.Eth.Blocks.GetBlockNumber.SendRequestAsync().Result.ToString());
        }

        public IEnumerable<string> GetNewTradeHashes(long? fromBlock = null)
        {
            return null;
        }
    }
}
