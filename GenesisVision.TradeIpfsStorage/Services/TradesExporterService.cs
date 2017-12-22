using GenesisVision.TradeIpfsStorage.Interfaces.Trades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GenesisVision.TradeIpfsStorage.Services
{
    internal class TradesExporterService
    {
        public static string ExportToCsv(IEnumerable<IMetaTraderOrder> trades, string text = null)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

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
                               $"\"{trade.DateOpen:G}\";" +
                               $"\"{trade.DateClose:G}\";" +
                               $"\"{trade.Direction}\";");
            }
            return csv.ToString();
        }
    }
}
