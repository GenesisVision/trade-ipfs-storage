namespace GenesisVision.TradeIpfsStorage.Interfaces
{
    public enum ServerType
    {
        Undefined = 0,
        MetaTrader4 = 1,
        MetaTrader5 = 2
    }

    public interface IServer
    {
        string Host { get; set; }
        string Name { get; set; }
        ServerType Type { get; set; }
    }
}
