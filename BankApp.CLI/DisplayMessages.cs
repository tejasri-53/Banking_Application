using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.CLI
{
    class Messages
    {
        public static void WelcomeMenu() 
        {
            Console.WriteLine("Choose an option...\n1)Add Bank \n 2)Create Staff Account \n 3)Login\n4) EXIT\n\nEnter your choice: "); 
        }
        public static void StaffLoginMenu()
        {
            Console.WriteLine("Choose an action...\n1) Create Account\n2) Update Account\n3) Delete Account\n4) Update sRTGS\n5) Update sIMPS\n6) Update oRTGS\n7) Update oIMPS\n8) Logout\n\nEnter your choice: ");
        }
        public static void TransactionModeMenu()
        {
            Console.WriteLine("Choose the type of transcation you want to perform: \n1)RTGS\n2)IMPS\n\nEnter your choice:");
        }
        public static void UpdateCustomerAccount()
        {
            Console.WriteLine("Choose an action...\n1) Update Name\n2) Update Password\n3) Back\n\nEnter your choice:");
        }
        public static void AskName()
        {
            Console.WriteLine(" Enter name: ");
        }

        public static void AskPassword()
        {
            Console.WriteLine(" Enter Password:");
        }

        public static void AskAccountId()
        {
            Console.WriteLine(" Enter the AccountId :");
        }
        public static void AskDepositAmount()
        {
            Console.WriteLine(" Enter the amount to be deposited: ");
        }

        public static void AskWithdrawAmount()
        {
            Console.WriteLine(" Enter the amount to withdraw: ");
        }

        public static void InvalIdAccountId()
        {
            Console.WriteLine(" InvlId Account Id !!! ");
        }

        public static void LoginMenu()
        {
            Console.WriteLine(" \n\n1)Deposit  \n 2)Transfer Money (INR only)\n3) Withdraw Money (INR only)\n4) Logout\n\nChoose an option:");
        }

        public static void InvalIdCredentials()
        {
            Console.WriteLine(" Wrong Id or Password... ");
        }

        public static void TransferAskId()
        {
            Console.WriteLine("Enter the Id of the account to whome u want to transfer ammount: ");
        }

        public static void AskTransferAmount()
        {
            Console.WriteLine("Enter the amount to be transfered: ");
        }
        public static string TransactionSuccess()
        {
            return ("Transaction sucessfully completed !!! ");
        }

        public static string  TransactionErrorInsufficientBal()
        {
            return ("Unable to complete transaction as there is insufficient balance. ");
        }

        public static void InsuffiecientFunds()
        {
            Console.WriteLine("Insufficient Funds.....");
        }
        public static void WithdrawError()
        {
            Console.WriteLine("An error occured while withdrawing...");
        }

        public static void TransactionFetchingError()
        {
            Console.WriteLine("An error occured while fetching Transactions...");
        }


    }
}