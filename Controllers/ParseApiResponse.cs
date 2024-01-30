using StockPortfolio.Models;
namespace StockPortfolio.Controllers{
    public class ParseApiResponse{
        
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
            return new Stock()
        }
        public Stock ParseApiResponseEndpoint(string response){
            /*	
                Global Quote	
                01. symbol	"IBM"
                02. open	"191.3100"
                03. high	"192.3896"
                04. low	"186.1600"
                05. price	"187.4200"
                06. volume	"9895941"
                07. latest trading day	"2024-01-26"
                08. previous close	"190.4300"
                09. change	"-3.0100"
                10. change percent	"-1.5806%"
                */
            var json = JsonDocument.Parse(response);
            JsonElement root = json.RootElement;

            List<StockData> stockDataList = new List<StockData>();
            if (root.TryGetProperty("Global Quote", out JsonElement metaData)) {
                string symbol = metaData.GetProperty("01. symbol").GetString();
                string open = metaData.GetProperty("02. Open").GetString();
                string high = metaData.GetProperty("03. high").GetString();
                string low = metaData.GetProperty("04. low").GetString();
                string price = metaData.GetProperty("05. price").GetString();
                string volume = metaData.GetProperty("06. volume").GetString();
                string ltd = metaData.GetProperty("07. latest trading day").GetString();
                string previousClose = metaData.GetProperty("08. previous close").GetString();
                string change = metaData.GetProperty("09. change").GetString();
                string percentChange = metaData.GetProperty("10. change percent").GetString();
            }
            return new Stock() {

            };
        }
    }
}
