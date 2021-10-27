using System;
using System.Collections.Generic;
using ATMApplication.CLI;
using BankApp.Model;
using BankApp.Service;

namespace BankApp.CLI
{

    class Program
    {

        private static readonly string bankName = "MoneyBank";
        static void Main(string[] args)
        {
            bool exit = false;

            

            BankService bankService = new BankService();
            bankService.AddBank(bankName);
            


            while (!exit)
            {
                Console.Clear();
                Console.Write(DisplayMessages.WelcomeMenu());

                Choices.MainChoice mainChoice = (Choices.MainChoice)Enum.Parse(typeof(Choices.MainChoice), Console.ReadLine());
                switch (mainChoice)
                {
                    case Choices.MainChoice.SetupBank:
                        Console.WriteLine("Enter Bank Name");
                        string name = Console.ReadLine();
                        bankService.AddBank(name);
                        
                        break;
                    case Choices.MainChoice.CreateStaffAccount:
                        Console.Clear();
                        Console.Write(DisplayMessages.EnterName());
                        string NAME = Console.ReadLine();
                        Console.Write(DisplayMessages.EnterPassword());
                        string PASSWORD = Console.ReadLine();
                        string Id = bankService.CreateStaffAccount(NAME,PASSWORD);
                        Console.Clear();
                        Console.WriteLine($"Bank account created with:\nAccount ID: {Id}\nPassword:{PASSWORD}");
                        Console.ReadLine();
                        break;
                    case Choices.MainChoice.Login:
                        Console.Clear();
                        string ID_LOGIN, PASSWORD_LOGIN;
                        try
                        {

                            Console.WriteLine("1) Bankstaff Login\n2) Customer Login");
                            int option = Convert.ToInt32(Console.ReadLine());
                            if (option == 1)
                            {
                                Console.Clear();
                                Console.Write(DisplayMessages.EnterID());
                                ID_LOGIN = Console.ReadLine();
                                Console.Write(DisplayMessages.EnterPassword());
                                PASSWORD_LOGIN = Console.ReadLine();
                                Console.WriteLine("Enter Bank Id: ");
                                string BankId = Console.ReadLine();

                                Console.Clear();
                                if (bankService.AuthenticateStaff(ID_LOGIN, PASSWORD_LOGIN,BankId))
                                {
                                    bool e = false;
                                    while (!e)
                                    {
                                        Console.Clear();
                                        Console.Write(DisplayMessages.StaffLoginChoice());
                                        Choices.StaffLoginChoice staffLoginChoice = (Choices.StaffLoginChoice)Enum.Parse(typeof(Choices.StaffLoginChoice), Console.ReadLine());
                                        switch (staffLoginChoice)
                                        {
                                            case Choices.StaffLoginChoice.CreateAccount:
                                                Console.Clear();
                                                Console.Write(DisplayMessages.EnterName());
                                                string NAME_CREATEACCOUNT = Console.ReadLine();
                                                Console.Write(DisplayMessages.EnterPassword());
                                                string PASSWORD_CREATEACCOUNT = Console.ReadLine();
                                                string ID_CREATEACCOUNT = bankService.CreateCustomerAccount(NAME_CREATEACCOUNT, PASSWORD_CREATEACCOUNT);
                                                Console.Clear();
                                                Console.WriteLine($"Bank account created with:\nAccount ID: {ID_CREATEACCOUNT}\nPassword:{PASSWORD_CREATEACCOUNT}");
                                                Console.ReadLine();
                                                
                                                break;

                                            case Choices.StaffLoginChoice.UpdateAccount:
                                                Console.Clear();
                                                Console.Write(DisplayMessages.UpdateCustomerAccount());
                                                Choices.UpdateCustomerAccountChoice updateCustomerAccountLoginChoice = (Choices.UpdateCustomerAccountChoice)Enum.Parse(typeof(Choices.UpdateCustomerAccountChoice), Console.ReadLine());
                                                switch (updateCustomerAccountLoginChoice)
                                                {
                                                    case Choices.UpdateCustomerAccountChoice.UpdateName:
                                                        Console.Clear();
                                                        Console.Write(DisplayMessages.EnterID());
                                                        string CustomerAccountID = Console.ReadLine();
                                                        Console.Write(DisplayMessages.EnterName());
                                                        string NewName = Console.ReadLine();
                                                        Console.WriteLine("Enter Bank Id: ");
                                                        string BankId1 = Console.ReadLine();
                                                        NewName = bankService.UpdateCustomerName(CustomerAccountID, NewName,BankId1);
                                                        Console.WriteLine($"Name has been updated to {NewName}");
                                                        Console.ReadLine();
                                                        break;
                                                    case Choices.UpdateCustomerAccountChoice.UpdatePassword:
                                                        Console.Clear();
                                                        Console.Write(DisplayMessages.EnterID());
                                                        string CustomerID = Console.ReadLine();
                                                        Console.Write(DisplayMessages.EnterPassword());
                                                        string NewPassword = Console.ReadLine();
                                                        Console.WriteLine("Enter Bank Id: ");
                                                        string bankId3 = Console.ReadLine();
                                                        NewPassword = bankService.UpdateCustomerPassword(CustomerID, NewPassword,bankId3);
                                                        Console.Write($"Password has been updated to {NewPassword}");
                                                        Console.ReadLine();
                                                        break;
                                                    case Choices.UpdateCustomerAccountChoice.Back:
                                                        Console.Clear();
                                                        break;
                                                }
                                                break;
                                            case Choices.StaffLoginChoice.DeleteAccount:
                                                Console.Clear();
                                                Console.Write(DisplayMessages.EnterID());
                                                string CustomerAccID = Console.ReadLine();
                                                Console.WriteLine("Enter Bank Id: ");
                                                string bankId = Console.ReadLine();
                                                if (bankService.DeleteCustomerAccount(CustomerAccID,bankId))
                                                {
                                                    Console.Write("Account Found and Deleted !!!");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Account not found in records...");
                                                }
                                                break;
         
                                            case Choices.StaffLoginChoice.UpdatesRTGS:
                                                Console.WriteLine("Enter new RTGS Service Charges for same bank:");
                                                int RTGS = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter new IMPS Service Charges for same bank:");
                                                int IMPS = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter Bank Id: ");
                                                string bankId1 = Console.ReadLine();
                                                bankService.UpdateSameBankCharges(bankId1, RTGS, IMPS);
                                                break;
                                            
                                            case Choices.StaffLoginChoice.UpdatedRTGS:
                                                Console.WriteLine("Enter new RTGS Service Charges for diffrent bank:");
                                                int rtgs= Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter new IMPS Service Charges for different bank:");
                                                int imps = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter Bank Id: ");
                                                string bankId2 = Console.ReadLine();
                                                bankService.UpdateDifferentBankCharges(bankId2, rtgs, imps);
                                                break;
                                            case Choices.StaffLoginChoice.ViewAccountTransaction:
                                                try
                                                {
                                                    Console.WriteLine("Enter Bank Id: ");
                                                    string bankId3 = Console.ReadLine();
                                                    Console.Clear();
                                                    string t = bankService.GetTransactions(ID_LOGIN,bankId3);
                                                    Console.Write(t);
                                                    Console.Write("\nPress Enter to exit...");
                                                    Console.ReadLine();

                                                }
                                                catch (Exception ee)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine(DisplayMessages.TransactionFetchingError() + ee.ToString());
                                                    Console.ReadLine();
                                                }
                                                break;
                                                
                                            
                                            case Choices.StaffLoginChoice.Logout:
                                                e = true;
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(DisplayMessages.InvalidCredentials());
                                    Console.ReadLine();
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.Write(DisplayMessages.EnterID());
                                ID_LOGIN = Console.ReadLine();
                                Console.Write(DisplayMessages.EnterPassword());
                                PASSWORD_LOGIN = Console.ReadLine();
                                Console.WriteLine("Enter Bank Id: ");
                                string bankId = Console.ReadLine();
                                Console.Clear();

                                if (bankService.AuthenticateCustomer(ID_LOGIN, PASSWORD_LOGIN,bankId))
                                {
                                    bool e = false;
                                    while (!e)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Enter Bank Id: ");
                                        string bankId1 = Console.ReadLine();
                                        string NAME_LOGIN = bankService.GetCustomerName(ID_LOGIN,bankId1);
                                        int balance = bankService.GetCustomerBalance(ID_LOGIN,bankId1);
                                        Console.WriteLine($"Welcome {NAME_LOGIN}");
                                        Console.WriteLine($"Your account balance is {balance}₹");

                                        Console.Write(DisplayMessages.LoginMenu());

                                        Choices.CustomerLoginChoice loginChoice = (Choices.CustomerLoginChoice)Enum.Parse(typeof(Choices.CustomerLoginChoice), Console.ReadLine());

                                        switch (loginChoice)
                                        {
                                            case Choices.CustomerLoginChoice.Deposit:
                                                Console.Clear();
                                                Console.Write(DisplayMessages.EnterID());
                                                string ID_DEPOSIT = Console.ReadLine();
                                                Console.Write(DisplayMessages.EnterDepositAmount());

                                                try
                                                {
                                                    int amount = Convert.ToInt32(Console.ReadLine());
                                                    Console.WriteLine("Enter Bank Id: ");
                                                    string BankId = Console.ReadLine();
                                                    string NAME_DEPOSIT = bankService.DepositAmount(ID_DEPOSIT, amount,BankId);
                                                    Console.Clear();
                                                    Console.WriteLine($"{amount}₹ have been deposited into {NAME_DEPOSIT} Account");
                                                    Console.ReadLine();
                                                    break;
                                                }
                                                catch
                                                {
                                                    Console.Write(DisplayMessages.InvalidAccountID());
                                                    break;
                                                }
                                            case Choices.CustomerLoginChoice.TransferMoney:
                                                Console.Write(DisplayMessages.TransferEnterID());
                                                string ID_TO = Console.ReadLine();
                                                Console.Write(DisplayMessages.EnterTransferAmount());
                                                int AMOUNT_TRANSFER = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter Bank Id: ");
                                                string bankId2 = Console.ReadLine();
                                                Console.Clear();
                                                if (bankService.TransferAmount(ID_LOGIN, ID_TO, AMOUNT_TRANSFER,bankId2))
                                                {
                                                    Console.Write(DisplayMessages.TransactionSuccess());
                                                    Console.ReadLine();

                                                }
                                                else
                                                {
                                                    Console.Write(DisplayMessages.TransactionErrorInsufficientBal());
                                                    Console.ReadLine();
                                                }
                                                break;
                                            case Choices.CustomerLoginChoice.Withdraw:
                                                Console.Write(DisplayMessages.EnterWithdrawAmount());
                                                int a = Convert.ToInt32(Console.ReadLine());
                                                try
                                                {
                                                    Console.WriteLine("Enter Bank Id: ");
                                                    string bankId3 = Console.ReadLine();
                                                    string check = bankService.WithdrawAmount(ID_LOGIN, a,bankId3);
                                                    if (check == "Failed")
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine(DisplayMessages.InsuffiecientFunds());
                                                        Console.ReadLine();
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine($"{a} has been withdrawed succesfully");
                                                        Console.ReadLine();
                                                    }
                                                }
                                                catch
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine(DisplayMessages.WithdrawError());
                                                    Console.ReadLine();
                                                }
                                                break;
                                            case Choices.CustomerLoginChoice.ShowTransactions:
                                                try
                                                {
                                                    Console.WriteLine("Enter Bank Id: ");
                                                    string bankId4 = Console.ReadLine();
                                                    Console.Clear();
                                                    string t = bankService.GetTransactions(ID_LOGIN,bankId4);
                                                    Console.Write(t);
                                                    Console.Write("\nPress Enter to exit...");
                                                    Console.ReadLine();

                                                }
                                                catch (Exception ee)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine(DisplayMessages.TransactionFetchingError() + ee.ToString());
                                                    Console.ReadLine();
                                                }
                                                break;
                                            case Choices.CustomerLoginChoice.Logout:
                                                e = true;
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.Write(DisplayMessages.InvalidCredentials());
                                    Console.ReadLine();
                                }
                            }

                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Enter a valid ID or Password");
                        }
                        break;
                    case Choices.MainChoice.EXIT:
                        exit = true;
                        break;
                }
            }
        }



    }
}
