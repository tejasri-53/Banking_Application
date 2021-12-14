using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.CLI
{
    class Messages
    {
        public static string WelcomeMenu = "Choose an option...\n1)Create Bank \n2) Login\n3) EXIT\n\nEnter your choice: ";


        public static string StaffLoginMenu = "Choose an action...\n1) Create Account\n2) Update Account\n3) Delete Account\n4) Update sRTGS\n5) Update sIMPS\n6) Update oRTGS\n7) Update oIMPS\n 8) Logout\nEnter your choice: ";


        public static string UpdateCustomerAccount = "Choose an action...\n1) Update Name\n2) Update Password\n3) Back\n\nEnter your choice: ";


        public static string AskName = "Enter name: ";

        public static string TransactionModeMenu = "Choose the type of transcation you want to perform: \n1)RTGS\n2)IMPS\n\nEnter your choice: ";


        public static string AskPassword = "Enter Password: ";


        public static string AskAccountId = "Enter the AccountId : ";


        public static string AskDepositAmount = "Enter the amount to be deposited: ";


        public static string AskWithdrawAmount = "Enter the amount to withdraw: ";


        public static string InvalIdAccountId = "InvlId Account Id !!!";


        public static string LoginMenu = "\n\n1) Transfer Money \n2) Withdraw Money \n3)Deposit\n4) Logout\n\nChoose an option:";


        public static string InvalIdCredentials = "Wrong Id or Password...";


        public static string TransferAskId = "Enter the Id of the account to whome u want to transfer ammount: ";


        public static string AskTransferAmount = "Enter the amount to be transfered: ";


        public static string TransactionSuccess = "Transaction sucessfully completed !!!";


        public static string TransactionErrorInsufficientBal = "Unable to complete transaction as there is insufficient balance.";


        public static string InsuffiecientFunds = "Insufficient Funds.....";


        public static string WithdrawError = "An error occured while withdrawing...";


        public static string TransactionFetchingError = "An error occured while fetching Transactions...";

    }
}
