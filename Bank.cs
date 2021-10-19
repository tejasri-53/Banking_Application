using System;
using System.Collections.Generic;
using BankApplication.Services;
using Transaction.Models;

namespace BankApplication
{
    class Bank
    {

        public static List<transactionModels> transactionList;
        Dictionary<string, AccountModel> AccountsList;

         Bank()
        {
              Dictionary<int, AccountModel>  AccountsList = new Dictionary<int, AccountModel>();

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
        public static bool Validate(string accountID, string pin)
        {
            Bank b = new Bank();
            if (pin != b.AccountsList[accountID].pin)
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
                bankAccount bankAcc = new bankAccount();
                Bank b = new Bank();
                Transactions t = new Transactions();
                switch (operations) 
                {
                    
                  

                    case Operations.CreateAccount:
                                        t.createAccount(b.AccountsList);
                                        break;
                    case Operations.Deposit:
                                            t.deposit(b.AccountsList);
                                            break;
                    case Operations.Withdraw:
                                            t.withdraw(b.AccountsList);
                                             break;
                    case Operations.Transfer: Transfer.TransferAmount(b.AccountsList,transactionList);
                                            break;
                    case Operations.BalanceEnquiry:  
                                            AccountModel Id = b.AccountsList[DisplayMessages.EnterAccountID()];
                                                bankAccount.BalanceEnquiry(Id) ;
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

            
            CreateAccount=1 ,
            Deposit,
            Withdraw,
            Transfer,
            BalanceEnquiry,
            Exit

        }






    }
}
