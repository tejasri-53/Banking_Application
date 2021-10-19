using System;

namespace BankApplication
{
    class DisplayMessages
    {
        
        public void Options()
        {
            Console.WriteLine("1. Create a Account");
            Console.WriteLine("2. Deposit Amount");
            Console.WriteLine("3. Withdraw Amount");
            Console.WriteLine("4. Transfer Amount");
            Console.WriteLine("5. Balance Enquiry");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Select Your Choice: ");
        }

        public static string EnterUserName()
        {
            Console.Write("Enter Your Name:\n ");
            return Console.ReadLine();
        }
        public static string EnterPIN()
        {
            Console.Write("Enter Your PIN: \n");
            return Console.ReadLine();

        }
        public static string  EnterAccountID()
        {
            Console.Write("Enter Your Account ID: \n");
            return (Console.ReadLine());
        }

        public static double EnterAmount()
        {
            Console.Write("Enter Amount To Deposit: \n");
            return Convert.ToDouble(Console.ReadLine());
        }

        public static void InvalidPIN()
        {
            Console.WriteLine("Invalid PIN\n");
        }

        public static void AccountDoesntExist()
        {
            Console.WriteLine("Account Doesn't Exist\n");
        }

        public static void Invalid()
        {
            Console.WriteLine("Invalid Choice\n");
        }
        public static void Exit()
        {
            Console.WriteLine("Successfully Exited\n");
        }


        public static double EnterWithdrawAmount()
        {
            Console.Write("Enter Amount To Withdraw:\n ");
            return Convert.ToDouble(Console.ReadLine());
        }

        public static void InsufficientAmount()
        {
            Console.WriteLine("Insufficient Amount\n");
        }

        public static void transactionhis() {
            Console.WriteLine("Transaction History\n");
        }
        public static void DepositMessage()
        {
            Console.WriteLine("Successfully Deposited\n");
        }
        public static void WithDrawMessage()
        {
            Console.WriteLine(" Amount Successfully Withdrawn\n");
        }
        public static void TransferMessage()
        {
            Console.WriteLine("Successfull Transaction of Transfer");
        }

        public static double EnterTransferAmount()
        {
            Console.Write("Enter Amount To Transfer: ");
            return Convert.ToDouble(Console.ReadLine());
        }
    }
}
