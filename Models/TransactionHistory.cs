using System;
using System.Collections.Generic;
using System.Text.Json;

namespace StockPortfolio.Models{
  public class TransactionHistory{
    private List<string> transactions = new List<string>();

    public void AddTransaction(string symbol, string name, int quantity, decimal price, DateTime date)
    {
      transactions.Add($"Symbol: {symbol}, Name: {name}, Quantity: {quantity}, Price: {price}, Date: {date}");
    } 

    public IEnumerable<string> GetTransactions() {
      return transactions;
    }
    public IEnumerable<string> GetTransactionsFromDateRange(DateTime startDate, DateTime endDate){
      return transactions.FindAll(transaction => {
          string[] parts = transaction.Split(',');
          //parses date of current transaction element
          DateTime date = DateTime.Parse(parts[4].Substring(6));
          return date >= startDate && date <= endDate;
          });
    }

    public decimal GetTotalTransactionValue(){
      decimal totalValue = 0;
      foreach (string transaction in transactions){
        string[] parts = transaction.Split(',');
        int quantity = int.Parse(parts[2]);
        decimal price = decimal.Parse(parts[3]);
        totalValue += quantity * price;
      }
      return totalValue;
    }

    public decimal GetAverageTransactionValue(){
      decimal totalValue = 0;
      foreach (string transaction in transactions){
        string[] parts = transaction.Split(',');
        int quantity = int.Parse(parts[2]);
        decimal price = decimal.Parse(parts[3]);
        totalValue += quantity * price;

      }

      if (transactions.Count > 0) {
        return totalValue/transactions.Count;
      }
      return 0;
    }
  }
}
