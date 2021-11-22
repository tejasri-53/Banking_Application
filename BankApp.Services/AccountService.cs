﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading.Tasks;
using BankApp.Models;
using BankApp.Models.Exceptions;

using System.Transactions;
using BankAppModels.Exceptions;
using Technovert.BankApp.Services;

namespace BankApp.Services
{
    public class AccountService
    {
        private readonly BankService bankService;
        public AccountService(BankService bankService)
        {
            this.bankService = bankService;

        }
        public string CreateAccount(string bankId, string accountHolderName, string password, decimal initialDeposit, bool gender)
        {

            Bank bank = bankService.SingleBank(bankId);

            Account account = new Account()
            {
                Name = accountHolderName,
                Id = GenerateAccountId(accountHolderName),
                Password = password,
                Balance = initialDeposit,
                isMale = gender,
                Transactions = new List<Transaction>(),

                
            };
            
            bank.Accounts.Add(account);
            bankService.savejson();

            return account.Id;
        }
        
        public bool UpdateAccount(string bankId, string accountId, string name, bool gender)
        {
            Bank bank = bankService.SingleBank(bankId);
            Account account = SingleAccount(bank, accountId);
            account.Name = name;
            account.isMale = gender;
            bankService.savejson();
            return true;
        }
        public bool DeleteAccount(string bankId, string accountId)
        {
            Bank bank = bankService.SingleBank(bankId);
            Account account = bank.Accounts.SingleOrDefault(account => account.Id == accountId);
            if (account == null)
            {
                throw new AccountNotFoundException("Account isn't found!");
            }

            bank.Accounts.Remove(account);
            bankService.savejson();
            return true;
        }
        public Account SingleAccount(Bank bank, string accountId)
        {
            Account account = bank.Accounts.SingleOrDefault(m => m.Id == accountId);
            if (account == null)
            {
                throw new AccountNotFoundException("Account is not Found in the Database.Please recheck your credentials or create a new account");
            }
            else
            {
                return account;
            }
        }
        public bool ValidatePassword(Account account, string password)
        {
            if (account.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string GenerateAccountId(string accountHolderName)
        {
            DateTime dt = new DateTime();
            string date = dt.ToShortDateString();
            return accountHolderName.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
        }
    }
}