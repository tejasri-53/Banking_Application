using System;
using System.Collections.Generic;



namespace BankApplication.Services
{
    public class bankAccount
    {

        public static void CreateAccount(Dictionary<string, AccountModel> AccountsList, string name, string pin)
        {

            AccountModel account = new AccountModel(name, pin);
            AccountsList.Add(account.getAccountId(), account);
        }

        public static void BalanceEnquiry(AccountModel account)
        {
            Console.WriteLine("Your Available Balance : " + account.GetAmount());
        }
        public static bool VerifyBalanceAmount(AccountModel account, double amount)
        {
            if (account.GetAmount() >= amount)
            {
                return true;
            }
            return false;
        }


        public static void Deposit(Dictionary<string, AccountModel> AccountsList, AccountModel account, double amount)
        {


            account.SetAmount(account.GetAmount() + amount);

        }



        public static void Withdraw(AccountModel account, double amount)
        {

            account.SetAmount(account.GetAmount() - amount);

            BalanceEnquiry(account);

        }



    }
}
