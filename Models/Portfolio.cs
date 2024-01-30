using System;
using System.Collections.Generic;
using System.Text.Json;
using StockPortfolio.Controllers;

namespace StockPortfolio.Models {

    public class Portfolio {
        private Dictionary<string, int> holdings = new Dictionary<string, int>();
        private decimal cashBalance;

        public Portfolio(decimal initialCash) {
            cashBalance = initialCash;
        }

        public decimal GetCashBalance(){
            return cashBalance;
        }

        public decimal GetTotalPortfolioValue(){
            decimal totalValue = cashBalance;

            foreach (var holding in holdings){
                Stock stock = GetStockInfo(holding.Key); //replace GetStockInfo with api info

                if (stock != null) {
                    totalValue += stock.Price*holding.Value;
                }
            }
            return totalValue;
        }

        public string GetPortfolioSummary()
        {
            List<object> summaryList = new List<object>();

            foreach (var holding in holdings) {
                Stock stock = GetStockInfo(holding.Key); //GetStockInfo will be replaced with the AlphaVantage api or other stock api info 

                if (stock != null) {
                    var summaryItem = new {
                        Symbol = stock.Symbol,
                        Name = stock.Name,
                        Quantity = holding.Value,
                        CurrentPrice = stock.Price,
                        TotalValue = stock.Price * holding.Value
                    };
                    summaryList.Add(summaryItem);
                }
            }
            decimal cashBalance = GetCashBalance();
            var portfolioSummary = new {
                Holdings = summaryList,
                CashBalance = cashBalance,
                TotalPortfolioValue = GetTotalPortfolioValue()
            };

            return JsonSerializer.Serialize(portfolioSummary, new JsonSerializerOptions { WriteIndented = true });
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
            if (holdings.TryGetValue(stock.Symbol, out int currentQuantity)){

                if (quantity <= holdings[stock.Symbol]) {
                    decimal totalCost = stock.Price * quantity;
                    holdings[stock.Symbol] = currentQuantity - quantity; 
                    cashBalance += totalCost;
                    Console.WriteLine($"Sold {quantity} shares of {stock.Symbol}");
                } 
                else {
                    Console.WriteLine($"Not enough shares of {stock.Symbol} to sell");
                }
            } 
            else {
                Console.WriteLine($"You do not own any {stock.Symbol}.");
            }
        }
    }
}
