using System;
using System.Collections.Generic;
using System.Text;
using GenesisVision.TradeIpfsStorage.Interfaces.Trades;

namespace GenesisVision.TradeIpfsStorage.Services
{
    internal class TradesExporterService
    {
        public static string ExportToCsv(IEnumerable<IMetaTraderOrder> trades, string text = null)
        {
            var csv = new StringBuilder(!string.IsNullOrEmpty(text)
                ? text
                : $"\"Login\";\"Ticket\";\"Symbol\";\"PriceOpen\";\"PriceClose\";\"Profit\";\"Volume\";\"DateOpen\";\"DateClose\";\"Direction\";{Environment.NewLine}");
            foreach (var trade in trades)
            {
                csv.AppendLine($"\"{trade.Login}\";" +
                               $"\"{trade.Ticket}\";" +
                               $"\"{trade.Symbol}\";" +
                               $"\"{trade.PriceOpen}\";" +
                               $"\"{trade.PriceClose}\";" +
                               $"\"{trade.Profit}\";" +
                               $"\"{trade.Volume}\";" +
                               $"\"{trade.DateOpen}\";" +
                               $"\"{trade.DateClose}\";" +
                               $"\"{trade.Direction}\";");
            }
            return csv.ToString();
        }
    }
}
