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
        public string? TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string? Type { get; set; }
        public double Amount { get; set; }
    }
}
