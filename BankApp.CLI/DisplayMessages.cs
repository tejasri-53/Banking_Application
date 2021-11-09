using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.CLI
{
    class DisplayMessages
    {
        public static string WelcomeMenu()
        {
            return "Choose an option...\n 1)Set up Bank \n 2)CreateStaffAccount \n  3)Login\n 4) EXIT\n\nEnter your choice: ";
        }

        public static string EnterName()
        {
            return "Enter your name: ";
        }

        public static string EnterPassword()
        {
            return "Enter a Password: ";
        }
        public static string StaffLoginChoice()
        {
            return "Choose an action...\n1) CreateAccount\n2) UpdateAccount\n3) DeleteAccount\n 4) UpdatesSameBankRTGS_IMPS Charges\n5) UpdateOtherBankRTGS_IMPS\n 6)AddCurrency \n 7)ViewTransaction History \n 8)Logout\n\nEnter your choice: ";
        }

        public static string UpdateCustomerAccount()
        {
            return "Choose an action...\n1) Update Name\n2) Update Password\n3) Back\n\nEnter your choice: ";
        }
        public static string EnterBankName()
        {
            return "Enter your bank Name:";
        }
        public static string EnterID()
        {
            return "Enter the AccountID : ";
        }

        public static string EnterDepositAmount()
        {
            return "Enter the amount to be deposited: ";
        }

        public static string EnterWithdrawAmount()
        {
            return "Enter the amount to withdraw: ";
        }

        public static string InvalidAccountID()
        {
            return "Invlid Account ID !!!";
        }

        public static string LoginMenu()
        {
            return "\n\n1) Transfer Money\n2)Deposit Amount \n 3) Withdraw Money\n4) Show Transactions\n5)Revert Transaction 6)Logout\n\nChoose an option:";
        }

        public static string InvalidCredentials()
        {
            return "Incorrect ID or Password...";
        }

        public static string TransferEnterID()
        {
            return "Enter the ID of the account to whome u want to transfer ammount: ";
        }

        public static string EnterTransferAmount()
        {
            return "Enter the amount to be transfered: ";
        }

        public static string TransactionSuccess()
        {
            return "Transaction sucessfully completed !!!";
        }

        public static string TransactionErrorInsufficientBal()
        {
            return "Unable to complete transaction as there is insufficient balance.";
        }

        public static string InsuffiecientFunds()
        {
            return "Insufficient Funds.....";
        }

        public static string WithdrawError()
        {
            return "An error occured while withdrawing...";
        }

        public static string TransactionFetchingError()
        {
            return "An error occured while fetching Transactions...";
        }
    }
}