using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using BankApp.Models;
using BankApp.Models.Exceptions;

using BankApp.Model;
using BankAppModels.Exceptions;
using BankApp.Services;
using BankAppModels.Enums;
using Technovert.BankApp.Services;

namespace BankApp.Services
{
    public class TransactionService
    {
        //AccountService as = new AccountService();
        public readonly BankService bankservice;
        public readonly AccountService accountservice;

        public TransactionService(BankService bankService, AccountService accountService)
        {
            this.bankservice = bankService;
            this.accountservice = accountService;
        }


        public bool Deposit(string bankId, string accountId, string password,  decimal deposit)
        {
            Bank bank = bankservice.SingleBank(bankId);
            Account account = accountservice.SingleAccount(bank, accountId);
            if (accountservice.ValidatePassword(account, password))
            {
                account.Balance += deposit;
                    Console.WriteLine(account.Balance);
                    bankservice.savejson();
                    return true;
          
            }
            else
            {
                throw new IncorrectPasswordException("Enter Correct Password! ");
            }


        }


        public bool Withdraw(string bankId, string accountId, string password, decimal withdraw)
        {
            Bank bank = bankservice.SingleBank(bankId);
            Account account = accountservice.SingleAccount(bank, accountId);
            if (accountservice.ValidatePassword(account, password))
            {
                if (account.Balance >= withdraw)
                {
                    account.Balance -= withdraw;
                    Console.WriteLine(account.Balance);
                    bankservice.savejson();
                    return true;
                }
                else
                {
                    throw new InsufficientBalanceException("Please Deposit some amount.Transaction failed due to insufficient Balance");
                }
            }
            else
            {
                throw new IncorrectPasswordException("Enter Correct Password! ");
            }


        }
        public bool TransferMoney(string senderBankId, string senderActId, string password, string recieverBankId, string receiverActId, TransactionCharge transactionCharge, decimal amountTransfered)
        {
            Bank senderBank = bankservice.SingleBank(senderBankId);
            Account senderAccount = senderBank.Accounts.SingleOrDefault(ac => ac.Id == senderActId);
            if (senderAccount == null)
            {
                throw new AccountNotFoundException("Account Not Found! Please Check");
            }
            Bank receiverBank = bankservice.SingleBank(recieverBankId);
            Account receiverAccount = senderBank.Accounts.SingleOrDefault(ac => ac.Id == receiverActId);
            if (receiverAccount == null)
            {
                throw new AccountNotFoundException("Account Not Found! Please Check");
            }
            decimal TaxPercentage = 0;
            if (transactionCharge == TransactionCharge.RTGS)
            {
                if (senderBank.Id == receiverBank.Id)
                {
                    TaxPercentage = senderBank.RTGSSameBank;
                }
                else
                {
                    TaxPercentage = senderBank.RTGSDiffBank;
                }
            }
            else if (transactionCharge == TransactionCharge.IMPS)
            {
                if (senderBank.Id == receiverBank.Id)
                {
                    TaxPercentage = senderBank.IMPSSameBank;
                }
                else
                {
                    TaxPercentage = senderBank.IMPSDiffBank;
                }
            }
            decimal amountDeducted = amountTransfered + (amountTransfered * TaxPercentage) / 100;
            if (accountservice.ValidatePassword(senderAccount, password))
            {
                if (senderAccount.Balance < amountDeducted)
                {
                    throw new InsufficientBalanceException("Please Deposit some amount. Transaction failed due to insufficient Balance");
                }
                else
                {
                    senderAccount.Balance -= amountDeducted;
                    receiverAccount.Balance += amountTransfered;
                    
                    /*string jsonTransaction = JsonSerializer.Serialize(transaction);
                    File.AppendAllText(@"F:\Visual Studio Code Projects\Technovert.BankApp\transactions.json", jsonTransaction);*/
                    Console.WriteLine(senderAccount.Balance);
                    Console.WriteLine(receiverAccount.Balance);
                    bankservice.savejson();
                    return true;
                }
            }
            else
            {
                throw new IncorrectPasswordException("Check your Password!");
            }
        }
         
         string GenerateTransactionId(string bankId, string accountId)
        {
            DateTime dt = new DateTime();
            if (bankId.Length < 3 || accountId.Length < 3)
            {
                throw new IncorrectArgumentRangeException(" BankId and AccountId Length must be greater than or equal to 3");
            }
            return "TXN" + bankId + accountId + dt.ToString("dd") + dt.ToString("MM") + dt.ToString("yyyy");
        }

    }
}