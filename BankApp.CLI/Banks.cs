using System;
using System.Collections.Generic;
using BankApp.Service;
using ConsoleTables;
using Microsoft.EntityFrameworkCore;

namespace BankApp.CLI
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            BankService bankService = new BankService();

            while (!exit)
            {
                ClearScreen();
                
                    MainMenu mainMenu = (MainMenu)Enum.Parse(typeof(MainMenu), GetString(Messages.WelcomeMenu));
                    switch (mainMenu)
                    {
                        case MainMenu.createBank:
                            ClearScreen();
                            string name = Console.ReadLine();
                             Console.WriteLine("Bank Created with BankId:" + bankService.AddBank(name));
                            break;

                        case MainMenu.Login:
                            ClearScreen();
                            string loginId, loginPassword;

                            int option = (int)GetNumber("1) Bankstaff Login \n2) Customer Login \n\nEnter Your Choice: ");

                            ClearScreen();
                            loginId = GetString(Messages.AskAccountId);

                            loginPassword = GetString(Messages.AskPassword);
                            ClearScreen();

                            if (option == 1)
                            {
                                if (bankService.AuthenticateStaff(loginId, loginPassword))
                                {
                                    bool e = false;
                                    while (!e)
                                    {
                                        ClearScreen();
                                        string ch = GetNumber(Messages.StaffLoginMenu).ToString();
                                        StaffLoginMenu StaffLoginMenu = (StaffLoginMenu)Enum.Parse(typeof(StaffLoginMenu), ch);
                                        switch (StaffLoginMenu)
                                        {
                                            case StaffLoginMenu.CreateAccount:
                                                ClearScreen();
                                                string createAccountBankId = GetString("Enter the BankId: ");
                                                string createAccountName = GetString(Messages.AskName);
                                                string createAccountPassword = GetString(Messages.AskPassword);
                                                string createAccountId = bankService.CreateCustomerAccount(createAccountName, createAccountPassword, createAccountBankId);
                                                ClearScreen();
                                                println($"Bank account created with:\nAccount Id: {createAccountId}\nPassword:{createAccountPassword}");
                                                Console.ReadLine();
                                                break;

                                            case StaffLoginMenu.UpdateAccount:
                                                ClearScreen();
                                                UpdateCustomerAccountMenu updateCustomerAccountLoginChoice = (UpdateCustomerAccountMenu)Enum.Parse(typeof(UpdateCustomerAccountMenu), GetNumber(Messages.UpdateCustomerAccount).ToString());
                                                switch (updateCustomerAccountLoginChoice)
                                                {
                                                    case UpdateCustomerAccountMenu.UpdateName:
                                                        ClearScreen();
                                                        string CustomerAccountId = GetString(Messages.AskAccountId);
                                                        string NewName = GetString(Messages.AskName);
                                                        NewName = bankService.UpdateCustomerName(CustomerAccountId, NewName);
                                                        println($"Name has been updated to {NewName}");
                                                        Console.ReadLine();
                                                        break;
                                                    case UpdateCustomerAccountMenu.UpdatePassword:
                                                        ClearScreen();
                                                        string CustomerId = GetString(Messages.AskAccountId);
                                                        string NewPassword = GetString(Messages.AskPassword);
                                                        NewPassword = bankService.UpdateCustomerPassword(CustomerId, NewPassword);
                                                        print($"Password has been updated to {NewPassword}");
                                                        Console.ReadLine();
                                                        break;
                                                    case UpdateCustomerAccountMenu.Back:
                                                        ClearScreen();
                                                        break;
                                                }
                                                break;
                                            case StaffLoginMenu.DeleteAccount:
                                                ClearScreen();
                                                string customerAccountId = GetString(Messages.AskAccountId);
                                                println(bankService.DeleteCustomerAccount(customerAccountId) ? "Account Found and Deleted !!!" : "Account not found in records...");
                                                break;
                                           
                                            case StaffLoginMenu.UpdatesRTGS:
                                                string bankId = GetString("Enter Bank Id:");
                                                float newsRTGS = GetNumber("Enter New sRTGS value: ");
                                                bankService.UpdatesRTGS(newsRTGS, bankId);
                                                print($"sRTGS updated to {newsRTGS}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.UpdatesIMPS:
                                                bankId = GetString("Enter Bank Id:");
                                                float newsIMPS = GetNumber("Enter New sRTGS value: ");
                                                bankService.UpdatesIMPS(newsIMPS, bankId);
                                                print($"sRTGS updated to {newsIMPS}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.UpdateoRTGS:
                                                bankId = GetString("Enter Bank Id:");
                                                float newoRTGS = GetNumber("Enter New sRTGS value: ");
                                                bankService.UpdateoRTGS(newoRTGS, bankId);
                                                print($"sRTGS updated to {newoRTGS}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.UpdateoIMPS:
                                                bankId = GetString("Enter Bank Id:");
                                                float newoIMPS = GetNumber("Enter New sRTGS value: ");
                                                bankService.UpdateoIMPS(newoIMPS, bankId);
                                                print($"sRTGS updated to {newoIMPS}");
                                                Console.ReadLine();
                                                break;
                                            
                                            
                                            case StaffLoginMenu.Logout:
                                                e = true;
                                                break;
                                            
                                        }
                                    }
                                }
                                else
                                {
                                    println(Messages.InvalIdCredentials);
                                    Console.ReadLine();
                                }
                            }
                            else
                            {

                                if (bankService.AuthenticateCustomer(loginId, loginPassword))
                                {
                                    bool e = false;
                                    while (!e)
                                    {
                                        ClearScreen();
                                        string loginName = bankService.GetName(loginId);
                                        float balance = bankService.GetBalance(loginId);
                                        println($"Welcome {loginName}");
                                        println($"Your account balance is {balance}₹");


                                        CustomerLoginMenu loginChoice = (CustomerLoginMenu)Enum.Parse(typeof(CustomerLoginMenu), GetString(Messages.LoginMenu));

                                        switch (loginChoice)
                                        {
                                            case CustomerLoginMenu.TransferMoney:
                                                int transferMoneyChoice = (int)GetNumber(Messages.TransactionModeMenu);
                                                TransactionModeMenu transactionModeMenu = (TransactionModeMenu)Enum.Parse(typeof(TransactionModeMenu), transferMoneyChoice.ToString());
                                                string toId = GetString(Messages.TransferAskId);
                                                float transferAmount = GetNumber(Messages.AskTransferAmount);
                                                ClearScreen();
                                                switch (transactionModeMenu)
                                                {
                                                    case TransactionModeMenu.RTGS:
                                                        println(bankService.TransferAmountRTGS(loginId, toId, transferAmount) ? Messages.TransactionSuccess : Messages.TransactionErrorInsufficientBal);
                                                        break;
                                                    case TransactionModeMenu.IMPS:
                                                        println(bankService.TransferAmountIMPS(loginId, toId, transferAmount) ? Messages.TransactionSuccess : Messages.TransactionErrorInsufficientBal);
                                                        break;
                                                }
                                                Console.ReadLine();
                                                break;
                                            case CustomerLoginMenu.Withdraw:
                                                float withdrawAmount = GetNumber(Messages.AskWithdrawAmount);

                                                bool check = bankService.WithdrawAmount(loginId, withdrawAmount);
                                                ClearScreen();
                                                println(check ? $"{withdrawAmount} has been withdrawed succesfully" : Messages.InsuffiecientFunds);
                                                Console.ReadLine();

                                                break;
                                            case CustomerLoginMenu.Deposit:
                                                ClearScreen();
                                                string depositId = GetString(Messages.AskAccountId);
                                                
                                                float amount = GetNumber(Messages.AskDepositAmount);
                                                string depositName = bankService.DepositAmount(depositId, amount);
                                                ClearScreen();
                                                println($"{amount}₹ have been deposited into {depositName} Account");
                                                Console.ReadLine();
                                                break;
                                        case CustomerLoginMenu.Logout:
                                                e = true;
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    ClearScreen();
                                    print(Messages.InvalIdCredentials);
                                    Console.ReadLine();
                                }
                            }
                            break;
                        case MainMenu.EXIT:
                            exit = true;
                            break;
                    }
                

            }
        }
    }
}
