using System.Text.Json.Serialization;

namespace BankAccountApp.Classes
{

    public class HandleBankDB
    {
        [JsonPropertyName("accounts")]
        public List<BankAccount> AllAccountsFromDB { get; set; } = new List<BankAccount>();
        public HandleBankDB() { }
    }
}
