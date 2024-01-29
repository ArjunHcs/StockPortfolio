namespace StockPortfolio.Models{
    public class TransactionHistory{
        private List<string> _transactions = new List<string>();

        public void AddTransaction(string symbol, string name, int quantity, decimal price, DateTime date)
        {
            _transactions.Add($"Symbol: {symbol}, Name: {name}, Quantity: {quantity}, Price: {price}, Date: {date}");
        } 

        public IEnumerable<string> GetTransactions() {
            return _transactions;
        }

        public IEnumerable<string> GetTransactionsFromDateRange(DateTime startDate, DateTime endDate){
            return _transactions.FindAll(transaction => {
                string[] parts = transaction.Split(',');
                //parses date of current transaction element
                DateTime date = DateTime.Parse(parts[4].Substring(6));
                return date >= startDate && date <= endDate;
            });
        }

        public decimal GetTotalTransactionValue(){
            decimal totalValue = 0;
            foreach (string transaction in _transactions){
                string[] parts = transaction.Split(',');
                int quantity = int.Parse(parts[2]);
                decimal price = decimal.Parse(parts[3]);
                totalValue += quantity * price;
            }
            return totalValue;
        }

        public decimal GetAverageTransactionValue(){
            decimal totalValue = 0;
            foreach (string transaction in _transactions){
                string[] parts = transaction.Split(',');
                int quantity = int.Parse(parts[2]);
                decimal price = decimal.Parse(parts[3]);
                totalValue += quantity * price;
            }
            if (_transactions.Count > 0) {
                return totalValue/_transactions.Count;
            }
            return 0;
        }
    }
}
