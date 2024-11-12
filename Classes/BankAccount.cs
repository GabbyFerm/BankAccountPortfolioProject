using BankAccountApp.Interface;
using System.Security.Principal;
using System.Transactions;

namespace BankAccountApp.Classes
{
    public class BankAccount : IAccount, ITransactionHistory
    {
        public string AccountType { get; set; }
        public int AccountNumber { get; set; }
        public string UserName { get; set; }
        public double Balance { get; set; }

        private List<Transaction> transactions = new List<Transaction>(); 
        public IReadOnlyList<Transaction> Transactions => transactions;
        private static int transactionCounter = 1;

        public BankAccount(string accountType, int accountNumber, string userName, double balance)
        {
            AccountType = accountType;
            AccountNumber = accountNumber;
            UserName = userName;
            Balance = balance;
            transactions = new List<Transaction>();
        }
        public void Deposit(double amountToDeposit)
        {
            if (amountToDeposit <= 0)
            {
                Console.WriteLine("Deposit amount must be greater than 0.");
                return;
            }

            Balance = Balance + amountToDeposit;

            Transaction newTransaction = new Transaction();
            newTransaction.TransactionId = "DP" + transactionCounter.ToString();
            newTransaction.Date = DateTime.Now;
            newTransaction.Type = "Deposit";
            newTransaction.Amount = amountToDeposit;

            transactions.Add(newTransaction);
            transactionCounter++;
        }
        public void Withdraw(double amountToDraw)
        {
            if (amountToDraw <= 0)
            {
                Console.WriteLine("Withdrawal amount must be greater than 0.");
                return;
            }

            Balance = Balance - amountToDraw;

            Transaction newTransaction = new Transaction();
            newTransaction.TransactionId = "WD" + transactionCounter.ToString();
            newTransaction.Date = DateTime.Now;
            newTransaction.Type = "Withdraw";
            newTransaction.Amount = amountToDraw;

            transactions.Add(newTransaction);
            transactionCounter++;
        }
        public void Transfer(double amountToTransfer, IAccount accountFrom, IAccount accountTo)
        {
            if (amountToTransfer <= accountFrom.Balance)
            {
                accountFrom.Withdraw(amountToTransfer);
                accountTo.Deposit(amountToTransfer);

                Console.WriteLine($"Successfully transferred {amountToTransfer} from account {accountFrom.AccountType} to account {accountTo.AccountType}.");
            }
            else
            {
                Console.WriteLine("Insufficient funds for the transfer.");
            }

            Transaction newTransaction = new Transaction();
            newTransaction.TransactionId = "TR" + transactionCounter.ToString();
            newTransaction.Date = DateTime.Now;
            newTransaction.Type = "Transfer";
            newTransaction.Amount = amountToTransfer;

            transactions.Add(newTransaction);
            transactionCounter++;
        }
        public void CheckBalance() 
        {
            Console.WriteLine($"Your balance in {AccountType} with accountnumber {AccountNumber} is: {Balance}.");
        }

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }
        public void ShowTransactionHistory()
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }
            foreach (var transaction in Transactions)
            {
                Console.WriteLine($"{transaction.TransactionId} - {transaction.Date} - {transaction.Type} - {transaction.Amount}");
            }
        }
    }
}
