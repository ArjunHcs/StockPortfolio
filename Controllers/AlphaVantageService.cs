using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace StockPortfolio.Controllers {

  public class AlphaVantageService {
    private const API_KEY = "API_KEY_HERE";
    private readonly HttpClient httpClient;
    public AlphaVantageService(){
      httpClient = new HttpClient();
    }
    public async Task<Stock> GetStockInfo(string symbol){
      string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=IBM&interval=5min&apikey={API_KEY}";
      Uri queryUri = new Uri(QUERY_URL);

      using (httpClient) {
        dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(httpClient.DownloadString(queryUri));
      }
    }
  }
}
