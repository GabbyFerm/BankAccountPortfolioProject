namespace BankAccountApp.Classes.Subclasses
{
    public class InvestmentAccount : BankAccount
    {
        public InvestmentAccount(string accountType, int accountNumber, string userName, double balance, List<Transaction> transactions)
        : base(accountType, accountNumber, userName, balance)
        {
        }
    }
}
