namespace StockPortfolio;
using System;
using System.Collections.Generic;

class Stock
{
  public string Symbol {get; set}
  public string Name {get; set }
  public double Price {get; set}
}

class Portfolio { 
  private Dictionary()<string, int> holdings = new Dictionary()<string, int>;
  private double cashBalance;

  public Portfolio(double initialCash){
    cashBalance = initialCash;
  }
  public void BuyStock(Stock stock, int quantity){
    double totalCost = stock.Price * quantity;

    if (cashBalance >= totalCost) {
      if (holdings.ContainsKey(stock.Symbol)){
        holdings[stock.Symbol] += quantity;
        }
      else {
        holdings.Add(stock.Symbol, quantity);
      cashBalance -= totalCost;
      Console.WriteLine($"Bought {quantity} shares of {stock.Symbol}");
        
      }
       
    }
    else {
      Console.WriteLine($"Not enough money to buy {quantity} shares of {stock.Symbol}");
    }

    public void SellStock(Stock stock, int quantity){
      
      double totalCost = stock.Price*quantity;
      if () {
      }
      else{
      }}

