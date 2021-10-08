using System;
using System.Collections.Generic;

using BankApplication.Services;

namespace BankApplication
{
    class Bank
    {
        
        public static Dictionary<int, Account> AccountsList;

        static Bank()
        {
            AccountsList = new Dictionary<int, Account>();

        }
        public static void Options()
        {
            Console.WriteLine("Customer Options: ");
            Console.WriteLine("1. Create a Account");
            Console.WriteLine("2. Deposit Amount");
            Console.WriteLine("3. Withdraw Amount");
            Console.WriteLine("4. Transfer Amount");
            Console.WriteLine("5. Balance Enquiry");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Select Your Choice: ");
        }
        public static bool Validate(int accountID, string pin)
        {
            if (pin != AccountsList[accountID].pin)
            {
                return false;
            }
            return true;
        }


        public static void Main()
        {
            while (true)
            {
                Bank.Options();
                Operations operations = (Operations) Enum.Parse(typeof(Operations),Console.ReadLine());
               
                switch (operations) 
                {
                    case Operations.CreateAccount:BankAccount.CreateAccount(AccountsList);
                                            break;
                    case Operations.Deposit:BankAccount.Deposit(AccountsList);
                                    break;
                    case Operations.Withdraw: BankAccount.Withdraw(AccountsList);
                                       break;
                    case Operations.Transfer:Transfer.TransferAmount(AccountsList);
                                     break;
                    case Operations.BalanceEnquiry:  Account Id = AccountsList[DisplayMessages.EnterAccountID()];
                                             BankAccount.BalanceEnquiry(Id) ;
                                            break;
                    case Operations.Exit: DisplayMessages.Exit();
                                         break;
                }
                if (operations == Operations.Exit) 
                {
                    break;
                }

            }
        }




        public enum Operations
        {
            CreateAccount = 1,
            Deposit,
            Withdraw,
            Transfer,
            BalanceEnquiry,
            Exit

        }






    }
}
