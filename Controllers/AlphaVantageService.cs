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
         
        public AlphaVantageService(){
            httpClient = new HttpClient();
        }
        public async Task<Stock> GetStockInfo(string symbol){
            string queryUrl =  $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval=5min&apikey={API_KEY}";

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
        public Stock ParseApiResponse(string response){
            string[] parts = response.Split(',');
            parts[0] = symbol;
            parts[1] = name;
            parts[2] = price;
            parts[3] = close;
            return new Stock(symbol, name, price, close) ;

        }
    }
}
