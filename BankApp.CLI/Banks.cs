using System;
using System.Collections.Generic;
using BankApp.Services;
using BankApp.Models;
using System.Text.Json;
using System.IO;
using BankAppModels.Exceptions;
using BankApp.CLI;
using System.Transactions;
using BankAppModels.Enums;
using ConsoleTables;

namespace BankApp.CLI
{
    public class Program
    {
        
           static void Main(string[] args)
            {
                bool exit = false;

                BankService bankService = new BankService();
                AccountService accountService = new AccountService();
                bankService.init();

                while (!exit)
                {
                    Console.Clear();
                    try
                    {
                        Messages.WelcomeMenu();
                        MainMenu mainMenu = (MainMenu)Enum.Parse(typeof(MainMenu), Console.ReadLine());
                        switch (mainMenu)
                        {

                            case MainMenu.CreateBank:
                                Console.WriteLine("Enter Bank Name:");
                                string bankName = Console.ReadLine();
                                string bankId = BankService.AddBank(bankName, 0, 5, 2, 6);
                                break;
                            case MainMenu.CreateStaffAccount:
                                Console.Clear();
                                Console.WriteLine("Enter Bank Id:");
                                string AccountBankId = Console.ReadLine();
                                Messages.AskName();
                                string AccountName = Console.ReadLine();
                                Messages.AskPassword();
                                string AccountPassword = Console.ReadLine();
                                string accountId = $"{AccountName.Substring(0, 3)}{DateTime.Now.ToString("ddMMyyyyHHmmss")}";
                            string AccountId = AccountService.CreateStaffAccount(accountId,AccountName, AccountPassword, AccountBankId);
                                Console.Clear();
                                Console.WriteLine($"Bank account created with:\nAccount Id: {AccountId}\nPassword:{AccountPassword}");
                                Console.ReadLine();
                                break;


                        case MainMenu.Login:
                                Console.Clear();
                                string loginId, loginPassword;
                                Console.WriteLine("1) Bankstaff Login \n2) Customer Login \n\nEnter Your Choice: ");
                                int option = Convert.ToInt32(Console.ReadLine());

                                Console.Clear();
                                Messages.AskAccountId();
                                loginId = Console.ReadLine();
                                Messages.AskPassword();
                                loginPassword = Console.ReadLine();
                                Console.Clear();

                                if (option == 1)
                                {
                                    if (AccountService.Authenticatestaff(loginId, loginPassword))
                                    {
                                        bool e = false;
                                        while (!e)
                                        {
                                            Console.Clear();
                                            Messages.StaffLoginMenu();
                                            string ch = Console.ReadLine();
                                            StaffLoginMenu StaffLoginMenu = (StaffLoginMenu)Enum.Parse(typeof(StaffLoginMenu), ch);
                                            switch (StaffLoginMenu)
                                            {
                                                case StaffLoginMenu.CreateAccount:
                                                    Console.Clear();
                                                    Console.WriteLine("Enter Bank Id:");
                                                    string createAccountBankId = Console.ReadLine();
                                                    Messages.AskName();
                                                    string createAccountName = Console.ReadLine();
                                                    Messages.AskPassword();
                                                    string createAccountPassword = Console.ReadLine();
                                                       
                                                    string createAccountId = AccountService.CreateCustomerAccount(createAccountName, createAccountPassword, createAccountBankId);
                                                    Console.Clear();
                                                    Console.WriteLine($"Bank account created with:\nAccount Id: {createAccountId}\nPassword:{createAccountPassword}");
                                                    Console.ReadLine();
                                                    break;

                                                case StaffLoginMenu.UpdateAccount:
                                                    Console.Clear();
                                                    Messages.UpdateCustomerAccount();
                                                    UpdateCustomerAccountMenu updateCustomerAccountLoginChoice = (UpdateCustomerAccountMenu)Enum.Parse(typeof(UpdateCustomerAccountMenu),Console.ReadLine());
                                                    switch (updateCustomerAccountLoginChoice)
                                                    {
                                                        case UpdateCustomerAccountMenu.UpdateName:
                                                            Console.Clear();
                                                            Messages.AskAccountId();
                                                            string CustomerAccountId =Console.ReadLine();
                                                            Messages.AskName();
                                                            string NewName = Console.ReadLine();
                                                            NewName = AccountService.UpdateCustomerName(CustomerAccountId, NewName);
                                                            Console.WriteLine($"Name has been updated to {NewName}");
                                                            Console.ReadLine();
                                                            break;
                                                        case UpdateCustomerAccountMenu.UpdatePassword:
                                                            Console.Clear();
                                                            Messages.AskAccountId();
                                                            string CustomerId = Console.ReadLine();
                                                            Messages.AskPassword();
                                                            string NewPassword = Console.ReadLine();
                                                            NewPassword = AccountService.UpdateCustomerPassword(CustomerId, NewPassword);
                                                            Console.WriteLine($"Password has been updated to {NewPassword}");
                                                            Console.ReadLine();
                                                            break;
                                                        case UpdateCustomerAccountMenu.Back:
                                                            Console.Clear();
                                                            break;
                                                    }
                                                    break;
                                                case StaffLoginMenu.DeleteAccount:
                                                    Console.Clear();
                                                    Messages.AskAccountId();
                                                    string customerAccountId = Console.ReadLine();
                                                    Console.WriteLine(AccountService.DeleteCustomerAccount(customerAccountId) ? "Account Found and Deleted !!!" : "Account not found in records...");
                                                    break;
                                                
                                                case StaffLoginMenu.UpdatesRTGS:
                                                    Console.WriteLine("Enter Bank Id:");
                                                    bankId = Console.ReadLine();
                                                    Console.WriteLine("Enter New sRTGS value: ");
                                                    float newsRTGS = Convert.ToInt32(Console.ReadLine());
                                                    float temp = BankService.UpdatesRTGS(newsRTGS, bankId);
                                                    Console.WriteLine($"sRTGS updated to {temp}");
                                                    Console.ReadLine();
                                                    break;
                                                case StaffLoginMenu.UpdatesIMPS:
                                                    Console.WriteLine("Enter Bank Id:");
                                                    bankId = Console.ReadLine();
                                                    Console.WriteLine("Enter New RTGS value:");
                                                    float newsIMPS = Convert.ToInt32(Console.ReadLine());
                                                    temp = BankService.UpdatesIMPS(newsIMPS, bankId);
                                                    Console.WriteLine($"sRTGS updated to {temp}");
                                                    Console.ReadLine();
                                                    break;
                                                case StaffLoginMenu.UpdateoRTGS:
                                                    Console.WriteLine("Enter Bank Id:");
                                                    bankId =Console.ReadLine();
                                                    Console.WriteLine("Enter New RTGS value:");
                                                    float newoRTGS = Convert.ToInt32(Console.ReadLine());
                                                    temp = BankService.UpdateoRTGS(newoRTGS, bankId);
                                                    Console.WriteLine($"sRTGS updated to {temp}");
                                                    Console.ReadLine();
                                                    break;
                                                case StaffLoginMenu.UpdateoIMPS:
                                                    Console.WriteLine("Enter Bank Id:");
                                                    bankId = Console.ReadLine();
                                                    Console.WriteLine("Enter New IMPS value: ");
                                                    int newoIMPS =Convert.ToInt32(Console.ReadLine()) ;
                                                    temp = BankService.UpdateoIMPS(newoIMPS, bankId);
                                                    Console.WriteLine($"sRTGS updated to {temp}");
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
                                        Messages.InvalIdCredentials();
                                        Console.ReadLine();
                                    }
                                }
                                else
                                {

                                    if (AccountService.AuthenticateCustomer(loginId, loginPassword))
                                    {
                                        bool e = false;
                                        while (!e)
                                        {
                                            Console.Clear();
                                            string loginName = BankService.GetName(loginId);
                                            float balance = AccountService.GetBalance(loginId);
                                            Console.WriteLine($"Welcome {loginName}");
                                            Console.WriteLine($"Your account balance is {balance}₹");

                                            Messages.LoginMenu();
                                            CustomerLoginMenu loginChoice = (CustomerLoginMenu)Enum.Parse(typeof(CustomerLoginMenu), Console.ReadLine());

                                            switch (loginChoice)
                                            {
                                                case CustomerLoginMenu.Deposit:
                                                    Console.Clear();
                                                    Messages.AskAccountId();
                                                    string depositId = Console.ReadLine();
                            
                                                    Messages.AskDepositAmount();
                                                    float amount = Convert.ToInt32(Console.ReadLine());
                                                    string depositName = TransactionService.DepositAmount(depositId, amount);
                                                    Console.Clear();
                                                    Console.WriteLine($"{amount}₹ have been deposited into {depositName} Account");
                                                    Console.ReadLine();
                                                    break;
                                                case CustomerLoginMenu.TransferMoney:
                                                        Messages.TransactionModeMenu();
                                                        int transferMoneyChoice = Convert.ToInt32(Console.ReadLine());
                                                        TransactionModeMenu transactionModeMenu = (TransactionModeMenu)Enum.Parse(typeof(TransactionModeMenu), transferMoneyChoice.ToString());
                                                        Messages.TransferAskId();
                                                        string toId = Console.ReadLine();
                                                        Messages.AskTransferAmount();
                                                        float transferAmount = Convert.ToInt32(Console.ReadLine());
                                                    Console.Clear();
                                                    switch (transactionModeMenu)
                                                        {
                                                            case TransactionModeMenu.RTGS:
                                                                Console.WriteLine(TransactionService.TransferAmountRTGS(loginId, toId, transferAmount) ? Messages.TransactionSuccess() : Messages.TransactionErrorInsufficientBal());
                                                                break;
                                                            case TransactionModeMenu.IMPS:
                                                      
                                                                Console.WriteLine(TransactionService.TransferAmountIMPS(loginId, toId, transferAmount) ? Messages.TransactionSuccess() : Messages.TransactionErrorInsufficientBal());
                                                                break;
                                                        }
                                                        Console.ReadLine();
                                                        break;
                                                    case CustomerLoginMenu.Withdraw:
                                                        Messages.AskWithdrawAmount();
                                                        float withdrawAmount =Convert.ToInt32(Console.ReadLine());

                                                        bool check = TransactionService.WithdrawAmount(loginId, withdrawAmount);
                                                        Console.Clear();
                                                        Console.WriteLine(check ? $"{withdrawAmount} has been withdrawed succesfully" : "Insufficient Funds") ;
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
                                        Console.Clear();
                                        Messages.InvalIdCredentials();
                                        Console.ReadLine();
                                    }
                                }
                                break;
                            case MainMenu.EXIT:
                                exit = true;
                                break;
                        }
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Error Occured");
                        Console.ReadLine();
                    }

                }
           }
     }
}
