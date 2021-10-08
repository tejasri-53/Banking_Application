using System;
using System.Collections.Generic;

namespace BankApplication
{
    class BankAccount
    {

        public static void CreateAccount(Dictionary<int, Account> AccountsList)
        {
            string name = DisplayMessages.EnterUserName();
            string pin = DisplayMessages.EnterPIN();
            Account account = new Account(name, pin);
            AccountsList.Add(account.GetAccountID(), account);
        }

        public static void BalanceEnquiry(Account account)
        {
            Console.WriteLine("Your Available Balance : " + account.GetAmount());
        }
        public static bool VerifyBalanceAmount(Account account, double amount)
        {
            if (account.GetAmount() >= amount)
            {
                return true;
            }
            return false;
        }


        public static void Deposit(Dictionary<int, Account> AccountsList)
        {
            int accountID = DisplayMessages.EnterAccountID();
            if (AccountsList.ContainsKey(accountID))
            {
                string pin = DisplayMessages.EnterPIN();
                if (Bank.Validate(accountID, pin))
                {
                    double amount = DisplayMessages.EnterAmount();
                    Account account = AccountsList[accountID];
                    account.SetAmount(account.GetAmount() + amount);
                    DisplayMessages.DepositMessage();
                    BalanceEnquiry(account);

                }
                else
                {
                    DisplayMessages.InvalidPIN();
                }
            }
            else
            {
                DisplayMessages.AccountDoesntExist();
            }

        }



        public static void Withdraw(Dictionary<int, Account> AccountsList)
        {
            int accountID = DisplayMessages.EnterAccountID();
            if (AccountsList.ContainsKey(accountID))
            {
                string pin = DisplayMessages.EnterPIN();
                if (Bank.Validate(accountID, pin))
                {
                    double amount = DisplayMessages.EnterWithdrawAmount();
                    Account account = AccountsList[accountID];
                    if (VerifyBalanceAmount(account, amount))
                    {
                        account.SetAmount(account.GetAmount() - amount);
                        DisplayMessages.WithDrawMessage();
                        BalanceEnquiry(account);

                    }
                    else
                    {
                        DisplayMessages.InsufficientAmount();
                    }

                }
                else
                {
                    DisplayMessages.InvalidPIN();
                }
            }
            else
            {
                DisplayMessages.AccountDoesntExist();
            }

        }



    }
}
