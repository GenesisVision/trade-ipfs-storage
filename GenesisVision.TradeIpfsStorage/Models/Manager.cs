using GenesisVision.TradeIpfsStorage.Interfaces;

namespace GenesisVision.TradeIpfsStorage.Models
{
    public class Manager: IManager
    {
        public string ManagerId { get; set; }

        internal Manager()
        {
        }

        public Manager(string managerId)
        {
            ManagerId = managerId;
        }
    }
}
