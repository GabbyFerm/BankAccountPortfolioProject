using System.Text.Json;
using Spectre.Console;
using Figgle;

namespace BankAccountApp.Classes
{
    public static class AccountOperations
    {

        public static void CheckBalance(HandleBankDB handleBankDB)
        {
            var accountChoices = handleBankDB.AllAccountsFromDB.Select(account => $"{account.AccountType} - {account.AccountNumber}").ToList();

            var selectedAccountChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an account to check balance:")
                    .PageSize(10) 
                    .AddChoices(accountChoices)
                    .HighlightStyle(new Style(Color.DarkTurquoise)) 
            );

            var selectedAccount = handleBankDB.AllAccountsFromDB.FirstOrDefault(account => $"{account.AccountType} - {account.AccountNumber}" == selectedAccountChoice);

            if (selectedAccount != null)
            {
                selectedAccount.CheckBalance(); 
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Account not found.[/]");
            }
        }

        public static void DepositMoney(string dataJSONfilePath, HandleBankDB handleBankDB)
        {
            var selectedAccountDepositTo = ChooseAccount(handleBankDB, "Choose an account to deposit money into:");

            if (selectedAccountDepositTo != null)
            {
                Console.WriteLine($"You selected account number {selectedAccountDepositTo.AccountNumber}. Proceeding with deposit.");

                double depositAmount = AnsiConsole.Ask<double>("[lightseagreen]Enter the amount to deposit:[/]");

                if (depositAmount <= 0)
                {
                    AnsiConsole.MarkupLine("[red]Deposit amount must be greater than 0.[/]");
                    return;
                }

                AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask("[lightseagreen]Transaction in progress...[/]");

                    while (!task.IsFinished)
                    {
                        task.Increment(10);
                        Thread.Sleep(100);
                    }

                    task.Description = "[lightseagreen]Transaction completed![/]";
                });

                selectedAccountDepositTo.Deposit(depositAmount);

                File.WriteAllText(dataJSONfilePath, JsonSerializer.Serialize(handleBankDB, new JsonSerializerOptions { WriteIndented = true }));

                Console.WriteLine($"You have successfully deposited {depositAmount:C} into account {selectedAccountDepositTo.AccountType}. New balance: {selectedAccountDepositTo.Balance:C}.");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Account not found.[/]");
            }
        }

        public static void ListAllAccounts(HandleBankDB handleBankDB)
        {
            AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("[lightseagreen]Loading accounts...[/]");

                while (!task.IsFinished)
                {
                    task.Increment(10); 
                    Thread.Sleep(100); 
                }

                task.Description = "[lightseagreen]Load complete![/]";
            });
            var table = new Table();

            table.AddColumn(new TableColumn("[bold lightseagreen]Account Type[/]").LeftAligned());
            table.AddColumn(new TableColumn("[bold lightseagreen]Account Number[/]").LeftAligned());
            table.AddColumn(new TableColumn("[bold lightseagreen]Balance[/]").LeftAligned());

            foreach (var account in handleBankDB.AllAccountsFromDB)
            {
                table.AddRow(account.AccountType, account.AccountNumber.ToString(), $"{account.Balance:C}");
            }

            table.Border = TableBorder.Rounded;
            table.LeftAligned();
            table.BorderColor(Color.LightSeaGreen);
            AnsiConsole.Write(table);
        }

        public static void TransferHistory(HandleBankDB handleBankDB)
        {
            var selectedAccountCheckTransferHistory = ChooseAccount(handleBankDB, "Choose an account to check transfer history:");

            if (selectedAccountCheckTransferHistory != null)
            {
                AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask("[lightseagreen]Loading account history...[/]");

                    while (!task.IsFinished)
                    {
                        task.Increment(10);
                        Thread.Sleep(100);
                    }

                    task.Description = "[lightseagreen]Load complete![/]";
                });

                selectedAccountCheckTransferHistory.ShowTransactionHistory();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Account not found.[/]");
            }
        }

        public static void TransferMoney(string dataJSONfilePath, HandleBankDB handleBankDB)
        {
            var selectedAccountTransferFrom = ChooseAccount(handleBankDB, "Choose the account to transfer money from:");
            if (selectedAccountTransferFrom == null)
            {
                AnsiConsole.MarkupLine("[red]No account selected.[/]");
                return;
            }

            var selectedAccountTransferTo = ChooseAccount(handleBankDB, "Choose the account to transfer money to:");
            if (selectedAccountTransferTo == null)
            {
                AnsiConsole.MarkupLine("[red]No account selected.[/]");
                return;
            }

            double transferAmount = ValidateAmountInput(selectedAccountTransferFrom.Balance);

            AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask("[lightseagreen]Transaction in progress...[/]");

                    while (!task.IsFinished)
                    {
                        task.Increment(10);
                        Thread.Sleep(100);
                    }

                    task.Description = "[lightseagreen]Transaction completed![/]";
                });

            selectedAccountTransferFrom.Transfer(transferAmount, selectedAccountTransferFrom, selectedAccountTransferTo);

            File.WriteAllText(dataJSONfilePath, JsonSerializer.Serialize(handleBankDB, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static void WithDrawMoney(string dataJSONfilePath, HandleBankDB handleBankDB)
        {
            var selectedAccountWithdrawFrom = ChooseAccount(handleBankDB, "Choose an account to withdraw money from:");

            if (selectedAccountWithdrawFrom != null)
            {
                Console.WriteLine($"You selected account number {selectedAccountWithdrawFrom.AccountNumber}. Proceeding with withdrawal.");

                double withdrawalAmount = ValidateAmountInput(selectedAccountWithdrawFrom.Balance);

                AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask("[lightseagreen]Transaction in progress...[/]");

                    while (!task.IsFinished)
                    {
                        task.Increment(10);
                        Thread.Sleep(100);
                    }

                    task.Description = "[lightseagreen]Transaction completed![/]";
                });

                selectedAccountWithdrawFrom.Withdraw(withdrawalAmount);

                File.WriteAllText(dataJSONfilePath, JsonSerializer.Serialize(handleBankDB, new JsonSerializerOptions { WriteIndented = true }));

                Console.WriteLine($"You have successfully withdrawn {withdrawalAmount:C} from account {selectedAccountWithdrawFrom.AccountType}. New balance: {selectedAccountWithdrawFrom.Balance:C}.");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Account not found.[/]");
            }
        }
        public static BankAccount? ChooseAccount(HandleBankDB handleBankDB, string promptMessage)
        {
            var accountChoices = handleBankDB.AllAccountsFromDB.Select(account => $"{account.AccountType} - {account.AccountNumber}").ToList();

            var selectedAccountChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(promptMessage)
                    .PageSize(10)
                    .AddChoices(accountChoices)
                    .HighlightStyle(new Style(Color.DarkTurquoise))
            );

            var selectedAccount = handleBankDB.AllAccountsFromDB.FirstOrDefault(account => $"{account.AccountType} - {account.AccountNumber}" == selectedAccountChoice);

            return selectedAccount; 
        }
        public static double ValidateAmountInput(double balance)
        {
            while (true)
            {
                Console.Write("Enter amount: ");
                string amountInput = Console.ReadLine()!;

                if (double.TryParse(amountInput, out double amount))
                {
                    if (amount > balance)
                    {
                        AnsiConsole.MarkupLine("[red]You don't have that much money in the account.[/]");
                    }
                    else if (amount <= 0)
                    {
                        AnsiConsole.MarkupLine("[red]The amount must be greater than zero.[/]");
                    }
                    else
                    {
                        return amount;
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Invalid input. Please enter a valid number.[/]");
                }
            }
        }
    }
}