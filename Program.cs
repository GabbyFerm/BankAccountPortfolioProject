using BankAccountApp.Classes;
using BankAccountApp.Classes.Subclasses;
using BankAccountApp.Interface;
using System.Text.Json;
using Spectre.Console;
using Figgle;

namespace BankAccountApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dataJSONfilePath = "JsonData/BankData.json";

            try
            {
                var handleBankDB = JsonSerializer.Deserialize<HandleBankDB>(File.ReadAllText(dataJSONfilePath));

                if (handleBankDB?.AllAccountsFromDB != null && handleBankDB.AllAccountsFromDB.Count > 0)
                {
                    bool appIsRunning = true;
                    string title = "Bank Account App";
                    string asciiArt = FiggleFonts.Ivrit.Render(title);
  
                    while (appIsRunning)
                    {
                        ClearConsoleShowHeadline(asciiArt);
                        string menuOptionChoosed = PrintOutUserMenu();
                        ClearConsoleShowHeadline(asciiArt);

                        switch (menuOptionChoosed)
                        {
                            case "List all accounts":
                                AccountOperations.ListAllAccounts(handleBankDB);
                                break;
                            case "Deposit money":
                                AccountOperations.DepositMoney(dataJSONfilePath, handleBankDB);
                                break;
                            case "Withdraw money":
                                AccountOperations.WithDrawMoney(dataJSONfilePath, handleBankDB);
                                break;
                            case "Transfer money":
                                AccountOperations.TransferMoney(dataJSONfilePath, handleBankDB);
                                break;
                            case "Check account balance":
                                AccountOperations.CheckBalance(handleBankDB);
                                break;
                            case "Show transaction history":
                                AccountOperations.TransferHistory(handleBankDB);
                                break;
                            case "Exit":
                                appIsRunning = false;
                                File.WriteAllText(dataJSONfilePath, JsonSerializer.Serialize(handleBankDB, new JsonSerializerOptions { WriteIndented = true }));
                                Console.WriteLine("Bank data saved. Exiting application.");
                                break;
                            default:
                                Console.WriteLine("Invalid option, try again.");
                                break;
                        }
                        if (appIsRunning)
                        {
                            PromptContinue();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No accounts found in the JSON data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void ClearConsoleShowHeadline(string asciiArt)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[bold cyan]{asciiArt}[/]");
            Console.WriteLine("Welcome to your bank!");
        }

        private static void PromptContinue()
        {
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
        private static string PrintOutUserMenu()
        {
            var menuOptions = new string[]
            {
                "List all accounts",
                "Deposit money",
                "Withdraw money",
                "Transfer money",
                "Check account balance",
                "Show transaction history",
                "Exit"
            };

            var menuSelection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an option:")
                    .PageSize(10) 
                    .AddChoices(menuOptions)
                    .HighlightStyle(new Style(Color.DarkTurquoise)) 
            );

            return menuSelection;
        }
    }
}
