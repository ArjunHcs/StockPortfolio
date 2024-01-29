namespace StockPortfolio.Models {
  public class Stock {
    public string Symbol { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal PreviousClose { get; set; }

    public Stock(string symbol, string name, decimal price, decimal previousClose)
    {
      Symbol = symbol;
      Name = name;
      Price = price;
      PreviousClose = previousClose;
    }
    public override string ToString()
    {
      return $"Symbol: {Symbol}, Name: {Name}, Price: {Price}, PreviousClose: {PreviousClose}";
    }
  }
}
