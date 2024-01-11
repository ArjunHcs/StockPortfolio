using System;
using System.Collections.Generic;

namespace StockPortfolio {

  class Stock {
    public string Symbol { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
  }

  class Portfolio {
    private Dictionary<string, int> holdings = new Dictionary<string, int>();
    private decimal cashBalance;

    public Portfolio(decimal initialCash) {
      cashBalance = initialCash;
    }

    public void BuyStock(Stock stock, int quantity) {
      decimal totalCost = stock.Price * quantity;

      if (cashBalance >= totalCost) {
        if (holdings.ContainsKey(stock.Symbol)) {
          holdings[stock.Symbol] += quantity;
        }
        else {
          holdings.Add(stock.Symbol, quantity);
          Console.WriteLine($"Bought {quantity} shares of {stock.Symbol}");
        }

        cashBalance -= totalCost;
      }
      else {
        Console.WriteLine($"Not enough money to buy {quantity} shares of {stock.Symbol}");
      }
    }

    public void SellStock(Stock stock, int quantity) {
      decimal totalCost = stock.Price * quantity;
      if (quantity <= holdings[stock.Symbol]) {
        holdings.Remove(stock.symbol, quantity);
        cashBalance += totalCost;
        Console.WriteLine($"Sold {quantity} shares of {stock.Symbol}");
      } else {
        Console.WriteLine($"Not enough shares of {stock.Symbol} to sell");
      }
    }

  }
  public class TransactionHistory{
    private List<string> transactions = new List<string>;
    public void AddTransaction(string symbol, string name, int quantity, decimal price, DateTime date)
    {
      transactions.Add($"Symbol: {symbol}, Name: {name}, Quantity: {quantity}, Price: {price}, Date: {date}");
    } 
    public IEnumerable<string> GetTransactions() {
      return transactions;
    }
    public decimal GetTotalTransactionValue(){
      decimal totalValue = 0;
      foreach (string transaction in transactions){
        string[] parts = transaction.Split(',');
        int quantity = int.Parse(parts[2]);
        decimal price = decimal.Parse(parts[3]);
        totalValue += quantity * price;
        return totalValue;
      }
    }
    public decimal GetAverageTransactionValue(){
      decimal totalValue = 0;
      foreach (string transaction in transactions){
        string[] parts = transaction.Split(',');
        int quantity = int.Parse(parts[2]);
        decimal price = decimal.Parse(parts[3]);
        totalValue += quantity * price;

        decimal average = totalValue/parts.Length;
        return average;
      }
    }



  }


}


