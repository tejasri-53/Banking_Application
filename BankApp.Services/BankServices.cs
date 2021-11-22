
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BankApp.Models;
using BankApp.Models.Exceptions;
using BankAppModels.Exceptions;

namespace Technovert.BankApp.Services
{
    public class BankService
    {
        private List<Bank> banks { get; set; }
        string jsonBanks = @"C:\Project\banks.json"; 
         
        
        public void deserializer() {
                string jsonstring = File.ReadAllText(jsonBanks);
                this.banks = JsonSerializer.Deserialize<List<Bank>>(jsonstring);
            
        }
        public string CreateBank(string name)
        {
            deserializer();
            if (!CheckBankExistsByName(name))
            {
                Bank bank = new Bank
                {
                    Id = this.GenerateRandomBankId(name),
                    Name = name,
                    Accounts = new List<Account>()
                };
                //banks.Add(bank);
                banks.Add(bank);
                string json = JsonSerializer.Serialize(banks);
                File.WriteAllText(jsonBanks, json);
                return bank.Id;
            }
            else
            {
                throw new BankCreationException("Bank Creation Failed! It seems Bank Already Exists");
            }
        }
        public void savejson() {
            string json = JsonSerializer.Serialize(banks);
            File.WriteAllText(jsonBanks, json);
        }
        public IDictionary<string, decimal> FindCurrencies(string bankId)
        {
            deserializer();
            Bank bank = this.banks.Find(m => m.Id == bankId);
            return bank.CurrenciesAccepted;
        }
        public string GetBankId(string name)
        {
            deserializer();
            Bank bank = this.banks.Find(m => m.Name == name);
            if (bank == null)
            {
                throw new BankNotFoundException("Bank Id can't be retrieved. Please check whether this bank exists");
            }
            return bank.Id;
        }
        public bool CheckBankExistsByName(string name)
        {
            deserializer();
            return this.banks.Any(b => b.Name == name);
        }
        public bool CheckBankExistsById(string id)
        {
            deserializer();
            return this.banks.Any(b => b.Id == id);
        }
        public Bank SingleBank(string bankId)
        {
            deserializer();
            Bank bank = this.banks.SingleOrDefault(m => m.Id == bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("The Bank details provided are incorrect. Check details again");
            }
            else
            {
                return bank;
            }
        }
        public bool AddNewCurrency(string bankId, string currencyName, decimal currencyValue)
        {
            deserializer();
            Bank bank = SingleBank(bankId);
            if (bank.CurrenciesAccepted.ContainsKey(currencyName))
            {
                throw new BankNotFoundException("Given Currency already exists ! Sorry Operation Cannot be performed");
            }
            bank.CurrenciesAccepted.Add(currencyName, currencyValue);
            string json = JsonSerializer.Serialize(banks);
            File.WriteAllText(jsonBanks, json);
            return true;
        }
        public bool AddServiceChargeSameBank(string bankId, decimal rtgs, decimal imps)//same method
        {
            deserializer();
            Bank bank = banks.SingleOrDefault(bank => bank.Id == bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("No Bank is found with the given ID");
            }
            bank.RTGSSameBank = rtgs;
            bank.IMPSSameBank = imps;
            string json = JsonSerializer.Serialize(banks);
            File.WriteAllText(jsonBanks, json);

            return true;
        }
        public bool AddServiceChargeDiffBank(string bankId, decimal rtgs, decimal imps)
        {
            deserializer();
            Bank bank = banks.SingleOrDefault(bank => bank.Id == bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("No Bank is found with the given ID");
            }
            bank.RTGSDiffBank = rtgs;
            bank.IMPSDiffBank = imps;
            string json = JsonSerializer.Serialize(banks);
            File.WriteAllText(jsonBanks, json);
            return true;
        }
        private string GenerateRandomBankId(string bankName)
        {

            DateTime dt = new DateTime();
            string date = dt.ToShortDateString();
            if (bankName.Length < 3)
            {
                throw new IncorrectArgumentRangeException("Length must be greater than or equal to 3");
            }
            else
            {
                return bankName.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
            }
        }
        
        public bool ExitApplication()
        {
            
            string json = JsonSerializer.Serialize(banks);
            File.WriteAllText(jsonBanks, json);
            return true;
        }
    }
}

