using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Model;
using BankApp.Models;
using static BankApp.Model.Account;

namespace BankApp.Service
{
    public class BankService
    {

        public Dictionary<string, Bank> banks = new Dictionary<string, Bank>();
        readonly Bank bankModel = new Bank("", 0, 5, 2, 6);

        static readonly DateTime PresentDate = DateTime.Today;

        public static string AccountIdPattern(string Name)
        {
            return Name.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
        }
        public bool AddBank(string name, int sameBankRTGS, int sameBankIMPS, int otherBankRTGS, int otherBankIMPS)
        {
            Bank bank = new Bank(name, sameBankRTGS, sameBankIMPS, otherBankRTGS, otherBankIMPS);
            this.banks.Add(bank.ID, bank);
            return true;
        }

        public bool AddBank(string name)
        {
            Bank bank = new Bank(name);
            this.banks.Add(bank.ID, bank);
            Console.WriteLine("Bank Set Up Successfully");
            return true;
        }

        public string CreateCustomerAccount(string name, string password)
        {
            Account acc = new Account(name);
            acc.Password = password;
            DateTime PresentDate = DateTime.Today;
            acc.BankId = name.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
            string AccountID = acc.AccountId;
            Bank bank = BankService.GetBank(acc.BankId);
            bank.customerAccounts.Add(AccountID, acc);
            return AccountID;
        }
        public string CreateStaffAccount(string name, string password)
        {
            StaffAccount acc = new StaffAccount(name, password);
            acc.Password = password;
            acc.AccountID = acc.AccountID;
            acc.Name = name;

            bankModel.staffAccounts.Add(acc.AccountID, acc);

            return name;
        }

        public string DepositAmount(string accountId, int amount, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);

            Account acc = bank.customerAccounts[accountId];

            acc.Balance += amount;
            string TID = acc.BankId + acc.AccountId + DateTime.Now.ToString("ddMMyyyy");
            Transaction tr = new Transaction(TID, accountId, amount, "Deposit", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            acc.AddTransaction(tr);
            return acc.Name;
        }

        public string WithdrawAmount(string accountID, int amount, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            Account acc = bank.customerAccounts[accountID];

            int bal = acc.Balance;
            if (bal < amount)
            {
                return "Failed";
            }

            acc.Balance = bal - amount;
            string TID = acc.BankId + acc.AccountId + DateTime.Now.ToString("ddMMyyyy");
            Transaction tr = new Transaction(TID, accountID, amount, "Withdraw", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc.AddTransaction(tr);
            return "";
        }

        public string GetName(string accountID, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            return bank.customerAccounts[accountID].Name;
        }

        public int GetBalance(string accountID, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            return bank.customerAccounts[accountID].Balance;
        }

        public bool TransferAmount(string fromId, string toId, int amount, string bankId)
        {
            int rtgsCharges;
            int impsCharges;
            Bank bank = BankService.GetBank(bankId);
            Account acc_from = bank.customerAccounts[fromId];
            Account acc_to = bank.customerAccounts[toId];
            Console.WriteLine("Enter Bank Id of from Account:");
            string BankName_from = Console.ReadLine();
            Console.WriteLine("Enter Bank Id of To Account:");
            string BankName_to = Console.ReadLine();
            if (BankName_from.Equals(BankName_to))
            {
                rtgsCharges = 0;
                impsCharges = (5 * (amount)) / 100;
            }
            else
            {
                rtgsCharges = (2 * (amount)) / 100;
                impsCharges = (6 * (amount)) / 100;
            }
            if (acc_from.Balance - amount < 0)
            {
                return false;
            }

            Transaction tr = new Transaction(acc_from.AccountId, acc_to.AccountId, amount, "Transfer", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc_from.AddTransaction(tr);
            Transaction trr = new Transaction(acc_from.AccountId, acc_to.AccountId, amount, "Transfer", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc_to.AddTransaction(trr);

            acc_from.Balance -= amount;
            acc_to.Balance += amount;
            acc_from.Balance -= rtgsCharges + impsCharges;
            acc_to.Balance -= rtgsCharges + impsCharges;

            return true;


        }
        public bool AuthenticateCustomer(string accountID, string password, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            Account acc = bank.customerAccounts[accountID];
            if (acc.Password == password)
            {
                return true;
            }
            return false;
        }

        public bool AuthenticateStaff(string accountID, string password, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            StaffAccount acc = bank.staffAccounts[accountID];
            if (acc.Password == password)
            {
                return true;
            }
            return false;
        }

        public string UpdateCustomerName(string accountID, string newName, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            Account acc = bank.customerAccounts[accountID];
            acc.Name = newName;
            return newName;
        }

        public string UpdateCustomerPassword(string accountID, string newPassword, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            Account acc = bank.customerAccounts[accountID];
            acc.Password = newPassword;
            return newPassword;
        }

        public bool DeleteCustomerAccount(string accountID, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            return bank.customerAccounts.Remove(accountID);
        }

        public string GetCustomerName(string accountID, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            return bank.customerAccounts[accountID].Name;
        }

        public int GetCustomerBalance(string accountID, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            return bank.customerAccounts[accountID].Balance;
        }
        public static Bank GetBank(string bankID)
        {
            BankService bankservice = new BankService();
            Bank bank = bankservice.banks[bankID];
            return bank;
        }

        public static void AddCurrency(string bankID, Currency currency)
        {
            BankService bankService = new BankService();
            Bank bank = bankService.banks[bankID];
            bank.AcceptedCurrency.Add(currency);
        }


        public void UpdateSameBankCharges(string bankID, int sameRTGS, int sameIMPS)
        {
            BankService bankService = new BankService();
            Bank bank = bankService.banks[bankID];
            bank.SameBankRTGSCharge = sameRTGS;
            bank.SameBankIMPSCharge = sameIMPS;
        }
        public void UpdateDifferentBankCharges(string bankID, int differentRTGS, int differentIMPS)
        {
            BankService bankService = new BankService();
            Bank bank = bankService.banks[bankID];
            bank.DifferentBankRTGSCharge = differentRTGS;
            bank.DifferentBankIMPSCharge = differentIMPS;
        }


        public static Currency GetCurrency(string bankID, string currencyCode)
        {
            BankService bankService = new BankService();
            Bank bank = bankService.banks[bankID];
            Currency currency = bank.AcceptedCurrency.SingleOrDefault(m => m.CurrencyCode == currencyCode);
            return currency;
        }

        public string GetTransactions(string ID, string bankId)
        {
            Bank bank = BankService.GetBank(bankId);
            string finaltransactions = "";
            Account acc = bank.customerAccounts[ID];
            List<Transaction> transactions = acc.GetTransactions();
            foreach (Transaction transaction in transactions)
            {
                finaltransactions += transaction.rID + " " + transaction.description + " " + transaction.amount + " " + transaction.time + "\n";
            }
            return finaltransactions;
        }

        public static string TransactionIdGenerator(string bankID, string accountID)
        {
            return "TXN" + bankID + accountID + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
        }




        public static void RevertTransaction(string bankID, string accountID)
        {
            Bank bank = BankService.GetBank(bankID);
            
            Account acc = bank.customerAccounts[accountID];
            List<Transaction> transactions = acc.GetTransactions();
            Transaction finaltransaction = transactions[-1];
            if (finaltransaction.description == "Withdraw")
            {
                double amount = finaltransaction.amount;
                string id = finaltransaction.rID;
                bank.customerAccounts[id].Balance = bank.customerAccounts[id].Balance + (int)amount;
            }
            else if (finaltransaction.description == "Deposit") {
                double amount = finaltransaction.amount;
                string id = finaltransaction.rID;
                bank.customerAccounts[id].Balance = bank.customerAccounts[id].Balance - (int)amount;
            }
            else
            {
                double amount = finaltransaction.amount;
                string id = finaltransaction.rID;
                string toId = finaltransaction.sID;
                bank.customerAccounts[id].Balance = bank.customerAccounts[id].Balance - (int)amount;
                bank.customerAccounts[toId].Balance = bank.customerAccounts[toId].Balance + (int)amount;
            }
            

        }
    }
}
