# BankAccountPortfolioProject
The Bank Account App is a C# console application that handles basic banking operations such as deposits, withdrawals, transfers, and viewing account balances and transaction history. The program saves data between sessions using JSON files and enhances user engagement with ASCII art and an interactive menu.

# Features
- List All Accounts: Displays a list of all registered accounts in the system.
- Deposit Money: Allows the user to deposit money into a selected account.
- Withdraw Money: Enables withdrawals from a selected account.
- Transfer Money: Facilitates transfers from one account to another.
- Check Account Balance: Shows the current balance of a selected account.
- Show Transaction History: Displays transaction history for each account.
- JSON-based Data Storage: All account information and transaction history are saved in a JSON file (BankData.json), ensuring data persistence across sessions.

# Technologies and Libraries
- C# .NET: Core technology used for developing the program.
- JSON (System.Text.Json): Used for reading and saving data.
- Spectre.Console: Handles advanced console output, such as colored text messages and interactive menus.
- Figgle: Used for generating ASCII art to create a visually engaging title screen.

# Principles and Design Patterns
- Object-Oriented Programming (OOP): The program follows object-oriented principles with classes for various account types and operations, such as AccountOperations, HandleBankDB, and subclasses for different account types. Focus on clean code.
- Interface Usage: The program includes an IAccount interface to define shared methods that can be implemented by various account types.
- Generic Methods: Several functions are designed to be reusable, such as input validation and menu option handling.
- JSON Data Storage: The use of JSON enables easy storage and retrieval of account information and transaction history, making the solution both readable and scalable.
- Spectre.Console for UI/UX: Using Spectre.Console, the user experience is enhanced with an interactive menu, colored texts, and clear feedback through progress bars and highlights.
- Error Handling: The program includes error handling to manage any issues that may arise during data retrieval or input, ensuring a user-friendly experience.
