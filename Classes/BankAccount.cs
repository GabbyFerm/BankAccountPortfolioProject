﻿using BankAccountApp.Interface;
using System.Security.Principal;
using System.Transactions;
using Spectre.Console;

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
                AnsiConsole.MarkupLine("[red]Deposit amount must be greater than 0.[/]");
                return;
            }

            Balance = Balance + amountToDeposit;

            Transaction newTransaction = new Transaction("DP" + transactionCounter.ToString(), DateTime.Now, "Deposit", amountToDeposit);

            transactions.Add(newTransaction);
            transactionCounter++;
        }
        public void Withdraw(double amountToDraw)
        {
            if (amountToDraw <= 0)
            {
                AnsiConsole.MarkupLine("[red]Withdrawal amount must be greater than 0.[/]");
                return;
            }

            Balance = Balance - amountToDraw;

            Transaction newTransaction = new Transaction("WD" + transactionCounter.ToString(), DateTime.Now, "Withdraw", amountToDraw);

            transactions.Add(newTransaction);
            transactionCounter++;
        }
        public void Transfer(double amountToTransfer, IAccount accountFrom, IAccount accountTo)
        {
            if (amountToTransfer <= accountFrom.Balance)
            {
                accountFrom.Withdraw(amountToTransfer);
                accountTo.Deposit(amountToTransfer);

                Console.WriteLine($"Successfully transferred {amountToTransfer:C} from account {accountFrom.AccountType} to account {accountTo.AccountType}.");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Insufficient funds for the transfer.[/]");
            }

            Transaction newTransaction = new Transaction("TR" + transactionCounter.ToString(), DateTime.Now, "Transfer", amountToTransfer);

            transactions.Add(newTransaction);
            transactionCounter++;
        }
        public void CheckBalance()
        {
            AnsiConsole.MarkupLine($"Your balance in [bold]{AccountType}[/] with account number [underline]{AccountNumber}[/] is: [lightseagreen]{Balance:C}[/].");
        }

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }
        public void ShowTransactionHistory()
        {
            if (Transactions == null || Transactions.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No transactions found.[/]");
                return;
            }
            var table = new Table();

            table.AddColumn(new TableColumn("[bold lightseagreen]Transaction ID[/]").LeftAligned());
            table.AddColumn(new TableColumn("[bold lightseagreen]Date[/]").LeftAligned());
            table.AddColumn(new TableColumn("[bold lightseagreen]Type[/]").LeftAligned());
            table.AddColumn(new TableColumn("[bold lightseagreen]Amount[/]").LeftAligned());

            foreach (var transaction in Transactions)
            {
                table.AddRow(
                             transaction.TransactionId.ToString(),
                             transaction.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                             transaction.Type,
                             $"{transaction.Amount:C}");
            }

            table.Border = TableBorder.Rounded;
            table.LeftAligned();
            table.BorderColor(Color.LightSeaGreen);
            AnsiConsole.Write(table);
        }
    }
}
