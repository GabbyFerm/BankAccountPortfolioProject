using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankAccountApp.Classes
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }

        public Transaction(string transactionId, DateTime date, string type, double amount)
        {
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId), "TransactionId cannot be null");
            Date = date;
            Type = type ?? throw new ArgumentNullException(nameof(type), "Type cannot be null");
            Amount = amount;
        }
    }
}
