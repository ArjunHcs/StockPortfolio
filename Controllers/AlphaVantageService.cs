using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using StockPortfolio.Models;
using System.Text.Json;

namespace StockPortfolio.Controllers {
    public class AlphaVantageService {
        private const string API_KEY = "API_KEY_HERE";
        private readonly HttpClient httpClient;

        public AlphaVantageService() {
            httpClient = new HttpClient();
        }

        public async Task<decimal> GetTickerCurrentPrice(string ticker) {
            string queryUrl = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={ticker}&apikey={API_KEY}";
            try {
                HttpResponseMessage response = await httpClient.GetAsync(queryUrl);
                if (response.IsSuccessStatusCode){
                    string json = await response.Content.ReadAsStringAsync();
                    var stockInfo = ParseApiResponseEndpoint(response);
                    return stockInfo;
                }
                else{
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            } catch (Exception e){

                Console.WriteLine($"Exception: {e.Message}");
                return null;
            }
        } 
        public async Task<Stock> GetStockInfo(string symbol) {
            string queryUrl =
                $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval=5min&apikey={API_KEY}";

            try {
                HttpResponseMessage response = await httpClient.GetAsync(queryUrl);

                if (response.IsSuccessStatusCode) {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var stockInfo = ParseApiResponse(jsonResponse);
                    return stockInfo;
                }
                else {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public Stock ParseApiResponse(string response) {
            var json = JsonDocument.Parse(response);
            JsonElement root = json.RootElement;

            if (root.TryGetProperty("Meta Data", out JsonElement metaData) &&
                    root.TryGetProperty("Time Series (5min)", out JsonElement timeSeries)) {

                string information = metaData.GetProperty("1. Information").GetString();
                string symbol = metaData.GetProperty("2. Symbol").GetString();
                string lastRefreshed = metaData.GetProperty("3. Last Refreshed").GetString();
                string interval = metaData.GetProperty("4. Interval").GetString();
                string outputSize = metaData.GetProperty("5. Output Size").GetString();
                string timeZone = metaData.GetProperty("6. Time Zone").GetString();

                List<StockData> stockDataList = new List<StockData>();
                foreach (var entry in timeSeries.EnumerateObject()) {
                    string timestamp = entry.Name;
                    JsonElement values = entry.Value;

                    decimal open = values.GetProperty("1. open").GetDecimal();
                    decimal high = values.GetProperty("2. high").GetDecimal();
                    decimal low = values.GetProperty("3. low").GetDecimal();
                    decimal close = values.GetProperty("4. close").GetDecimal();
                    int volume = values.GetProperty("5. volume").GetInt32();

                    StockData stockData = new StockData {
                        Timestamp = timestamp,
                                  Open = open,
                                  High = high,
                                  Low = low,
                                  Close = close,
                                  Volume = volume
                    };
                    stockDataList.Add(stockData);
                }

            }

            return new Stock {

            };
        }
        public DateTime ParseTimestamp (string timestamp){
            // dateString example = "2024-01-19 19:55:00";
            string format = "yyyy-MM-dd HH:mm:ss";

            if (DateTime.TryParseExact(timestamp, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                return DateTime.
            }
        }
    }
}
