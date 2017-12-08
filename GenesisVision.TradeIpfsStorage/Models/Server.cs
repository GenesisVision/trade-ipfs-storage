using GenesisVision.TradeIpfsStorage.Interfaces;

namespace GenesisVision.TradeIpfsStorage.Models
{
    public class Server : IServer
    {
        public string Host { get; set; }
        public string Name { get; set; }
        public ServerType Type { get; set; }

        internal Server()
        {
        }

        public Server(string host, string name, ServerType type)
        {
            Host = host;
            Name = name;
            Type = type;
        }
    }
}
