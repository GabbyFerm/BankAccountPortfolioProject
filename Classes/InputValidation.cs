using BankAccountApp.Interface;
using BankAccountApp;
namespace BankAccountApp.Classes
{
    public class InputValidation
    {

        public static BankAccount ValidateAccountNumber(List<BankAccount> allAccounts)
        {
            int accountChoice = -1;
            while (true)
            {
                string chosenAccount = Console.ReadLine()!;

                // Kontrollera att inmatningen är ett heltal
                if (chosenAccount.All(char.IsDigit))
                {
                    accountChoice = int.Parse(chosenAccount); // Konvertera input till ett heltal

                    // Hitta kontot i listan
                    var selectedAccount = allAccounts.FirstOrDefault(account => account.AccountNumber == accountChoice);

                    if (selectedAccount != null)
                    {
                        return selectedAccount; // Retur det valda kontot
                    }
                    else
                    {
                        Console.WriteLine("Invalid account number. That does not exist. Please enter a valid account number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid account number.");
                }
            }
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
                        Console.WriteLine("You don't have that much money in the account.");
                    }
                    else if (amount <= 0)
                    {
                        Console.WriteLine("The amount must be greater than zero.");
                    }
                    else
                    {
                        return amount;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }

        public static double ValidateNumericInput()
        {
            while (true)
            {
                Console.Write("Enter amount: ");
                string input = Console.ReadLine()!;

                if (double.TryParse(input, out double amount))
                {
                    if (amount <= 0)
                    {
                        Console.WriteLine("The amount must be greater than 0.");
                    }
                    else
                    {
                        return amount;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }
    }
}