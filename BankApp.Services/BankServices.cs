using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Model;
using BankApp.Models;
using static BankApp.Model.Account;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.Json.Serialization;


namespace BankApp.Service
{
    public class BankService
    {

       
        public Dictionary<string, float> currency = new Dictionary<string, float>();
        readonly Bank bankModel = new Bank("", 0, 5, 2, 6);

        static readonly DateTime PresentDate = DateTime.Today;
        public bool AddBank(string name, int sameBankRTGS, int sameBankIMPS, int otherBankRTGS, int otherBankIMPS)
        {
            Bank bank = new Bank(name, sameBankRTGS, sameBankIMPS, otherBankRTGS, otherBankIMPS);
            
            string json = JsonSerializer.Serialize(bank);
            File.AppendAllText(@"C:\Users\91990\source\repos\BankApp1\BankApp1\bank.json", json);
            return true;
        }


        public static string AccountIdPattern(string Name)
        {
            return Name.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
        }
        

        public bool AddBank(string name)
        {
            Bank bank = new Bank(name);
            try
            {
                
                
                string json = JsonSerializer.Serialize(bank);
                File.AppendAllText(@"C:\Users\91990\source\repos\BankApp1\BankApp.Services\bank.json", json);
                Console.WriteLine("Bank Set up Successfully");

            }
            catch {
                Console.WriteLine("Bank Already Exists");
            }
            
            return true;
        }

        public string CreateCustomerAccount(string name, string password,string bankName)
        {
            Account acc = new Account(name, password);
            acc.Password = password;
            acc.AccountId = name.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
            acc.Name = name;
            acc.BankId = bankName.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
            string json = JsonSerializer.Serialize(acc);
            File.AppendAllText(@"C:\Users\91990\source\repos\BankApp1\BankApp.Services\CustomerAccounts.json", json);
            Console.WriteLine($"Bank account created with:\nAccount ID: {acc.AccountId}\nPassword:{password}\n BankId:{acc.BankId}");

            return acc.AccountId;
        }
        public string CreateStaffAccount(string name, string password,string bankName)
        {
            StaffAccount acc = new StaffAccount(name, password);
            acc.Password = password;
            acc.AccountID = name.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
            acc.Name = name;
            acc.BankId = bankName.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
            string json = JsonSerializer.Serialize(acc);
            File.AppendAllText(@"C:\Users\91990\source\repos\BankApp1\BankApp.Services\StaffAccounts.json", json);
            Console.WriteLine($"Bank account created with:\nAccount ID: {acc.AccountID}\nPassword:{password}\n BankId:{acc.BankId}");
            return acc.AccountID;
        }

        public string DepositAmount(string accountId, int amount, string bankId,string currencyName)
        {
            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(jsonstring);
            string TID;
            Transaction tr;
            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountId == accountId)
                    {
                        acc.Balance += amount * (currency[currencyName]);
                        TID = acc.BankId;
                        tr = new Transaction(TID, accountId, amount, "Deposit", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                        acc.AddTransaction(tr);
                        string json = JsonSerializer.Serialize(accounts);
                        return acc.Name;

                    }
                }
            }
            return "";
        }

        public string WithdrawAmount(string accountID, int amount, string bankId)
        {
            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(jsonstring);
            string TID;
            Transaction tr;
            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountId == accountID)
                    {
                        acc.Balance  -= amount;
                        TID = acc.BankId + acc.AccountId + DateTime.Now.ToString("ddMMyyyy");
                        tr = new Transaction(TID, accountID, amount, "Withdraw", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        acc.AddTransaction(tr);
                        string json = JsonSerializer.Serialize(accounts);
                        return acc.Name;

                    }
                }
            }
            return "";
        }

        public string GetName(string accountID, string bankId)
        {
            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<StaffAccount> accounts = JsonSerializer.Deserialize<List<StaffAccount>>(jsonstring);
            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountID == accountID)
                    {
                        return acc.Name;

                    }
                }
            }
            return "";
        }

        public float GetBalance(string accountID, string bankId)
        {
            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(jsonstring);

            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountId == accountID)
                    {
                        return acc.Balance;

                    }
                }
            }
            return 0;
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
            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(jsonstring);
            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountId == toId)
                    {
                        acc.Balance += amount;
                        acc_to.Balance -= rtgsCharges + impsCharges;
                        string json = JsonSerializer.Serialize(accounts);

                    }
                }
            }
            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountId == fromId)
                    {
                        acc.Balance -= amount;
                        
                        string json = JsonSerializer.Serialize(accounts);

                        acc_from.Balance -= rtgsCharges + impsCharges;
                    }
                }
            }
            

            return true;


        }
        public bool AuthenticateCustomer(string accountID, string password, string bankId)
        {

            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<StaffAccount> accounts = JsonSerializer.Deserialize<List<StaffAccount>>(jsonstring);

            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountID == accountID)
                    {
                        if (acc.Password == password)
                        {
                            string json = JsonSerializer.Serialize(acc);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool AuthenticateStaff(string accountID, string password, string bankId)
        {
            
            string filename = "StaffAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<StaffAccount> accounts = JsonSerializer.Deserialize<List<StaffAccount>>(jsonstring);

            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountID == accountID)
                    {
                        if (acc.Password == password) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public string UpdateCustomerName(string accountID, string newName, string bankId)
        {
            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<StaffAccount> accounts = JsonSerializer.Deserialize<List<StaffAccount>>(jsonstring);

            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountID == accountID)
                    {
                        acc.Name = newName;
                        break;
                    }
                }
            }
            
            return newName;
        }

        public string UpdateCustomerPassword(string accountID, string newPassword, string bankId)
        {
            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<StaffAccount> accounts = JsonSerializer.Deserialize<List<StaffAccount>>(jsonstring);

            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountID == accountID)
                    {
                        acc.Password = newPassword;
                        break;
                    }
                }
            }
            
            return newPassword;
        }

        public bool DeleteCustomerAccount(string accountID, string bankId)
        {
            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(jsonstring);

            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountId == accountID)
                    {
                        accounts.Remove(acc);
                        return true;
                    }
                }
            }
            return false;
        }

        public string GetCustomerName(string accountID, string bankId)
        {
            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<StaffAccount> accounts = JsonSerializer.Deserialize<List<StaffAccount>>(jsonstring);
             foreach (var acc in accounts)
             {
                 if (acc.BankId == bankId)
                 {
                        if (acc.AccountID == accountID)
                        {
                            return acc.Name;

                        }
                 }
             }
            return "";
        }

        public float GetCustomerBalance(string accountID, string bankId)
        {
            string filename = "CustomerAccounts.json";
            string jsonstring = File.ReadAllText(filename);
            List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(jsonstring);

            foreach (var acc in accounts)
            {
                if (acc.BankId == bankId)
                {
                    if (acc.AccountId == accountID)
                    {
                        return acc.Balance;

                    }
                }
            }
            return 0;
        }
        public static Bank GetBank(string bankID)
        {
            string filename = "bank.json";
            string jsonstring = File.ReadAllText(filename);
            List<Bank> banks = JsonSerializer.Deserialize<List<Bank>>(jsonstring);
            foreach (var bank in banks)
            {
                if (bank.ID == bankID)
                {

                    return bank;
                }
            }
            return ;
        }

        public bool AddCurrency(string name, float value)
        {
            currency.Add(name, value);
            Console.WriteLine("Curency Added Successfully");
            return true;
        }


        public void UpdateSameBankCharges(string bankID, int sameRTGS, int sameIMPS)
        {
            
            string filename = "bank.json";
            string jsonstring = File.ReadAllText(filename);
            List<Bank> banks = JsonSerializer.Deserialize<List<Bank>>(jsonstring);
            foreach(var bank in banks) {
                if (bank.ID == bankID) 
                {
                    
                    bank.SameBankRTGSCharge = sameRTGS;
                    bank.SameBankIMPSCharge = sameIMPS;
                }
            }

        }
        public void UpdateDifferentBankCharges(string bankID, int differentRTGS, int differentIMPS)
        {
            
            string filename = "bank.json";
            string jsonstring = File.ReadAllText(filename);
            List<Bank> banks = JsonSerializer.Deserialize<List<Bank>>(jsonstring);
            foreach (var bank in banks)
            {
                if (bank.ID == bankID)
                {
                    bank.DifferentBankRTGSCharge = differentRTGS;
                    bank.DifferentBankIMPSCharge = differentIMPS;

                }
            }
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
