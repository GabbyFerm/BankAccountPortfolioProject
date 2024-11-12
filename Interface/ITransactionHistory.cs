using BankAccountApp.Classes;

namespace BankAccountApp.Interface
{
    public interface ITransactionHistory
    {
        IReadOnlyList<Transaction> Transactions { get;}
        void AddTransaction(Transaction transaction);
        void ShowTransactionHistory();
    }
}
