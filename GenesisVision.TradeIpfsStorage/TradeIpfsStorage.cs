using GenesisVision.PlatformContract;
using GenesisVision.TradeIpfsStorage.Interfaces;
using GenesisVision.TradeIpfsStorage.Interfaces.Trades;
using GenesisVision.TradeIpfsStorage.Models;
using GenesisVision.TradeIpfsStorage.Services;
using Nethereum.Geth;
using Nethereum.JsonRpc.Client;
using Nethereum.Web3.Accounts.Managed;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;

namespace GenesisVision.TradeIpfsStorage
{
    public class TradeIpfsStorage : ITradeIpfsStorage
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly PlatformContractService platformContractService;
        private readonly IpfsService ipfsService;

        private readonly string brokerAddress;
        
        private readonly Dictionary<long, Manager> managersByLogin;
        private readonly Dictionary<string, long> managersById;

        private readonly HashSet<long> managersQueue;
        private readonly object managersQueueLock;

        private readonly Timer updateContractTimer;

        public TradeIpfsStorage(string ipfsHost, string gethHost, string contractAddress, string brokerAddress, string brokerPassword, string serverId)
        {
            logger.Info($"Initialize GenesisVision.TradeIpfsStorage...");

            logger.Info($"IPFS host: {ipfsHost}");
            logger.Info($"Geth host: {gethHost}");
            logger.Info($"Broker manager account: {brokerAddress}");
            logger.Info($"Contract Address: {contractAddress}");
            logger.Info($"Server id: {serverId}");

            var web3 = new Web3Geth(new ManagedAccount(brokerAddress, brokerPassword), gethHost);

            platformContractService = new PlatformContractService(web3, contractAddress);
            ipfsService = new IpfsService(ipfsHost);
            managersByLogin = new Dictionary<long, Manager>();
            managersById = new Dictionary<string, long>();

            this.brokerAddress = brokerAddress;
            
            managersQueue = new HashSet<long>();
            managersQueueLock = new object();
            updateContractTimer = new Timer(60 * 1000);
            updateContractTimer.Elapsed += (sender, args) => UploadNewTradesToContract();

            InitManagers(serverId);
            updateContractTimer.Start();
        }

        #region Init

        private void InitManagers(string serverId)
        {
            // todo: getting managers from GV server
            var managers = new List<string>
                           {
                               "m1",
                               "8E916E5F-C12C-488A-B298-438CB4F51A75",
                               "AE4DB832-2D99-4971-A74E-A4B2D6FEB3C1",
                               "CD16E803-9BCC-41EB-AE78-6E559ACB95FF"
                           };
            logger.Debug($"Loaded {managers.Count} manager ids");

            foreach (var managerId in managers)
            {
                var managersLogin = platformContractService.GetManagerLoginAsyncCall(managerId).Result;
                if (!string.IsNullOrEmpty(managersLogin) && long.TryParse(managersLogin, out var login))
                {
                    var ipfsHash = platformContractService.GetManagerHistoryIpfsHashAsyncCall(managerId).Result;
                    managersById[managerId] = login;
                    managersByLogin[login] = new Manager {ManagerId = managerId, IpfsHash = ipfsHash};
                }
            }

            logger.Debug($"Loaded {managersById.Count} managers: {string.Join(", ", managersById.Select(x => $"{x.Key} ({x.Value})"))}");
        }

        #endregion

        #region Store new trades

        public void StoreNewTrades(IEnumerable<IMetaTraderOrder> trades)
        {
            var tradesByLogin = trades.GroupBy(x => x.Login,
                                          (k, v) => new
                                                    {
                                                        Login = k,
                                                        Orders = v.OrderByDescending(x => x.DateClose).ToList()
                                                    })
                                      .ToList();
            foreach (var tradeData in tradesByLogin.Where(x => managersByLogin.ContainsKey(x.Login)))
            {
                var manager = managersByLogin[tradeData.Login];

                logger.Debug($"New {tradeData.Orders.Count} trades. Manager: {manager.ManagerId} ({tradeData.Login}), prev ipfs: {manager.IpfsHash}");

                var lastHistoryHash = manager.IpfsHash;
                var tradeHistory = !string.IsNullOrEmpty(lastHistoryHash)
                    ? ipfsService.GetTextDataFromIpfs(lastHistoryHash)
                    : "";

                var history = TradesExporterService.ExportToCsv(tradeData.Orders, tradeHistory);

                var ipfsHash = ipfsService.UploadTrades(history, manager.ManagerId);

                manager.IpfsHash = ipfsHash;

                lock (managersQueueLock)
                {
                    managersQueue.Add(tradeData.Login);
                }
            }
        }

        public void UploadNewTradesToContract()
        {
            logger.Debug("Start upload trades to contract");
            try
            {
                List<long> tmpManagers;
                lock (managersQueueLock)
                {
                    tmpManagers = managersQueue.ToList();
                    managersQueue.Clear();
                }

                foreach (var login in tmpManagers)
                {
                    var manager = managersByLogin[login];

                    logger.Debug($"Start updating contract. Manager: {manager.ManagerId} ({login}), ipfs: {manager.IpfsHash}");
                    var hash = platformContractService.UpdateManagerHistoryIpfsHashAsync(brokerAddress, manager.ManagerId,
                        manager.IpfsHash, new HexBigInteger(300000)).Result;
                    logger.Info($"Contract updated. Manager: {manager.ManagerId} ({login}), ipfs: {manager.IpfsHash}, tx hash: {hash}");
                }
            }
            catch (Exception e)
            {
                logger.Error("Error at UploadNewTradesToContract. " + e.StackTrace);
            }
        }

        #endregion
    }
}
