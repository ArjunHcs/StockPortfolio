using System;
using System.Collections.Generic;

namespace StockPortfolio {

  class Stock {
    public string Symbol { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
  }

  class Portfolio {
    private Dictionary<string, int> holdings = new Dictionary<string, int>();
    private double cashBalance;

    public Portfolio(double initialCash) {
      cashBalance = initialCash;
    }

    public void BuyStock(Stock stock, int quantity) {
      double totalCost = stock.Price * quantity;

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

      double totalCost = stock.Price * quantity;
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
    public void AddTransaction(string transactionType, string stockSymbol, string stockName, int stockPrice, int shareQuantity){
      transactions.Add($"TransactionType: {transactionType}, Symbol: {stockSymbol}, Name: {stockName}, Price : {stockPrice}, Shares: {shareQuantity}");
    }

  }


}


