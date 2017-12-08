using System;
using GenesisVision.TradeIpfsStorage.Interfaces;
using GenesisVision.TradeIpfsStorage.Models;
using Newtonsoft.Json.Converters;

namespace GenesisVision.TradeIpfsStorage
{
    public static class JsonConverters
    {
        public class TradeContainerConverter : CustomCreationConverter<ITradeContainer>
        {
            public override ITradeContainer Create(Type objectType)
            {
                return new TradeContainer();
            }
        }

        public class ServerConverter : CustomCreationConverter<IServer>
        {
            public override IServer Create(Type objectType)
            {
                return new Server();
            }
        }

        public class TradeConverter : CustomCreationConverter<IBaseTrade>
        {
            public override IBaseTrade Create(Type objectType)
            {
                return new Trade();
            }
        }
    }
}
