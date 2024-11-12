using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountApp.Classes;

namespace BankAccountApp.Interface
{
    public interface IAccount
    {
        int AccountNumber { get; set; }
        double Balance { get; set; }
        string AccountType { get; set; }

        void Deposit(double amount);
        void Withdraw(double amount);
        void Transfer(double amount, IAccount targetAccount, IAccount accountTo);
        void CheckBalance();
    }
}
