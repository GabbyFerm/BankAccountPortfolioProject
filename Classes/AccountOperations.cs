using System.Text.Json;
using Spectre.Console;
using Figgle;

namespace BankAccountApp.Classes
{
    public static class AccountOperations
    {

        public static void CheckBalance(HandleBankDB handleBankDB)
        {
            Console.WriteLine("\nEnter the account for wich you want to check balance:");
            var selectedAccountCheckBalance = InputValidation.ValidateAccountNumber(handleBankDB.AllAccountsFromDB);

            AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("[green]Loading account...[/]");

                while (!task.IsFinished)
                {
                    task.Increment(10);
                    Thread.Sleep(100);
                }

                task.Description = "[green]Load complete![/]";
            });

            selectedAccountCheckBalance.CheckBalance();
        }

        public static void DepositMoney(string dataJSONfilePath, HandleBankDB handleBankDB)
        {
            Console.WriteLine("\nEnter the account number of the account to deposit money to:");
            var selectedAccountDepositTo = InputValidation.ValidateAccountNumber(handleBankDB.AllAccountsFromDB);

            Console.WriteLine($"You selected account number {selectedAccountDepositTo.AccountNumber}. Proceeding with deposit.");

            double depositAmount = InputValidation.ValidateNumericInput();

            AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("[green]Transaction in progress...[/]");

                while (!task.IsFinished)
                {
                    task.Increment(10); 
                    Thread.Sleep(100); 
                }

                task.Description = "[green]Transaction completed![/]";
            });

            selectedAccountDepositTo.Deposit(depositAmount);

            File.WriteAllText(dataJSONfilePath, JsonSerializer.Serialize(handleBankDB, new JsonSerializerOptions { WriteIndented = true }));

            Console.WriteLine($"You have successfully deposited {depositAmount} into account {selectedAccountDepositTo.AccountType}. New balance: {selectedAccountDepositTo.Balance}.");
        }

        public static void ListAllAccounts(HandleBankDB handleBankDB)
        {
            AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("[green]Loading accounts...[/]");

                while (!task.IsFinished)
                {
                    task.Increment(10); 
                    Thread.Sleep(100); 
                }

                task.Description = "[green]Load complete![/]";
            });
            Console.WriteLine("\nThese are your accounts:");
            foreach (var account in handleBankDB.AllAccountsFromDB)
            {
                Console.WriteLine($"Account Type: {account.AccountType}");
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Balance: {account.Balance}");
            }
        }

        public static void TransferHistory(HandleBankDB handleBankDB)
        {
            Console.WriteLine("\nEnter the account for wich you want to check transfer history:");
            var selectedAccountCheckTransferHistory = InputValidation.ValidateAccountNumber(handleBankDB.AllAccountsFromDB);
            AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("[green]Loading account history...[/]");

                while (!task.IsFinished)
                {
                    task.Increment(10);
                    Thread.Sleep(100);
                }

                task.Description = "[green]Load complete![/]";
            });
            selectedAccountCheckTransferHistory.ShowTransactionHistory();
        }

        public static void TransferMoney(string dataJSONfilePath, HandleBankDB handleBankDB)
        {
            Console.WriteLine("\nEnter the account number to transfer money from:");
            var selectedAccountTransferFrom = InputValidation.ValidateAccountNumber(handleBankDB.AllAccountsFromDB);

            Console.WriteLine($"Enter the account number to transfer money to:");
            var selectedAccountTransferTo = InputValidation.ValidateAccountNumber(handleBankDB.AllAccountsFromDB);

            double transferAmount = InputValidation.ValidateAmountInput(selectedAccountTransferFrom.Balance);

            AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("[green]Transaction in progress...[/]");

                while (!task.IsFinished)
                {
                    task.Increment(10); 
                    Thread.Sleep(100); 
                }

                task.Description = "[green]Transaction completed![/]";
            });

            selectedAccountTransferFrom.Transfer(transferAmount, selectedAccountTransferFrom, selectedAccountTransferTo);

            File.WriteAllText(dataJSONfilePath, JsonSerializer.Serialize(handleBankDB, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static void WithDrawMoney(string dataJSONfilePath, HandleBankDB handleBankDB)
        {
            Console.WriteLine("\nEnter the account number of the account to withdraw money from:");
            var selectedAccountWithdrawFrom = InputValidation.ValidateAccountNumber(handleBankDB.AllAccountsFromDB);

            Console.WriteLine($"You selected account number {selectedAccountWithdrawFrom.AccountNumber}. Proceeding with withdrawal.");

            double withdrawalAmount = InputValidation.ValidateAmountInput(selectedAccountWithdrawFrom.Balance);

            AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("[green]Transaction in progress...[/]");

                while (!task.IsFinished)
                {
                    task.Increment(10); 
                    Thread.Sleep(100); 
                }

                task.Description = "[green]Transaction completed![/]";
            });

            selectedAccountWithdrawFrom.Withdraw(withdrawalAmount);

            File.WriteAllText(dataJSONfilePath, JsonSerializer.Serialize(handleBankDB, new JsonSerializerOptions { WriteIndented = true }));

            Console.WriteLine($"You have successfully withdrawn {withdrawalAmount} from account {selectedAccountWithdrawFrom.AccountType}. New balance: {selectedAccountWithdrawFrom.Balance}.");
        }
    }
}