using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Model;
using ConsoleTables;

namespace BankApp.Service
{
    public class BankService
    {

        private MyDbContext dbContext;

        public BankService()
        {
            dbContext = new MyDbContext();
        }

        private string GetDateTimeNow(bool forId)
        {
            return forId ? DateTime.Now.ToString("ddMMyyyyHHmmss") : DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }


        public string AddBank(string name, float sRTGS, float sIMPS, float oRTGS, float oIMPS)
        {
            var bank = new Bank
            {
                BankId = $"{name.Substring(0, 3)}{DateTime.Now.ToString("ddMMyyyy")}",
                BankName = name,
                sRTGSCharge = sRTGS,
                sIMPSCharge = sIMPS,
                oRTGSCharge = oRTGS,
                oIMPSCharge = oIMPS
            };
            dbContext.Add(bank);
            dbContext.SaveChanges();
            return bank.BankId;
        }

        public string AddBank(string name)
        {
            var bank = new Bank
            {
                BankId = $"{name.Substring(0, 3)}{DateTime.Now.ToString("ddMMyyyy")}",
                BankName = name,
                sRTGSCharge = 0,
                sIMPSCharge = 5,
                oRTGSCharge = 2,
                oIMPSCharge = 6
            };
            dbContext.Add(bank);
            dbContext.SaveChanges();
            return bank.BankId;
        }

        public string CreateCustomerAccount(string name, string pass, string bankId)
        {
            var customer = new CustomerAccount
            {
                AccountId = $"{name.Substring(0, 3)}{GetDateTimeNow(true)}",
                Name = name,
                Password = pass,
                BankId = bankId,
                Balance = 0,
                IsActive=1
            };
            dbContext.Add(customer);
            dbContext.SaveChanges();
            return customer.AccountId;
        }

        public string CreateStaffAccount(string name, string pass, string bankId)
        {
            var staffAccounts = new StaffAccounts
            {
                AccountId = $"{name.Substring(0, 3)}{GetDateTimeNow(true)}",
                Name = name,
                Password = pass,
                BankId = bankId
            };
            dbContext.Add(staffAccounts);
            dbContext.SaveChanges();
            return staffAccounts.AccountId;
        }

        public string DepositAmount(string accountId, float amount)
        {
            var customer = dbContext.CustomerAccounts.Find(accountId);
            float currentBalance = customer.Balance;
            customer.Balance = currentBalance + amount;
            dbContext.SaveChanges();
           
            return customer.Name;
        }

        public bool WithdrawAmount(string accountId, float amount)
        {
            var customer = dbContext.CustomerAccounts.Find(accountId);
            float currentBalance = customer.Balance;
            if (currentBalance - amount >= 0)
            {
                customer.Balance = currentBalance - amount;
                dbContext.SaveChanges();
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AuthenticateCustomer(string accountId, string pass)
        {
            var customer = dbContext.CustomerAccounts.Find(accountId);
            return customer.Password == pass;
        }

        public bool AuthenticateStaff(string accountId, string pass)
        {
           
                var staff = dbContext.StaffAccounts.Find(accountId);
                return staff.Password == pass;
            
        }

        public string UpdateCustomerName(string accountId, string newName)
        {

            var customer = dbContext.CustomerAccounts.Find(accountId);
            customer.Name = newName;
            dbContext.SaveChanges();
            return newName;
        }

        public string UpdateCustomerPassword(string accountId, string newPassword)
        {
            var customer = dbContext.CustomerAccounts.Find(accountId);
            customer.Password = newPassword;
            dbContext.SaveChanges();
            return newPassword;
        }

        public bool DeleteCustomerAccount(string accountId)
        {
            var customer = dbContext.CustomerAccounts.Find(accountId);
            dbContext.Remove(customer);
            dbContext.SaveChanges();
            return true;
        }

        public string GetName(string accountId)
        {
            var customer = dbContext.CustomerAccounts.Find(accountId);
            return customer.Name;
        }

        public float GetBalance(string accountId)
        {
            var customer = dbContext.CustomerAccounts.Find(accountId);
            return customer.Balance;
        }

        


        public bool TransferAmountRTGS(string fromId, string toId, float amount)
        {
            var fromCustomer = dbContext.CustomerAccounts.Find(fromId);
            var toCustomer = dbContext.CustomerAccounts.Find(toId);

            if (fromCustomer.Balance - amount >= 0)
            {
                float fromBalance = fromCustomer.Balance;
                fromCustomer.Balance = fromBalance - amount;
                float toBalance = toCustomer.Balance;
                _ = toCustomer.BankId == fromCustomer.BankId ? toCustomer.Balance = toBalance + amount - (amount * dbContext.Banks.Find(toCustomer.BankId).sRTGSCharge) : toCustomer.Balance = toBalance + amount - (amount * dbContext.Banks.Find(fromCustomer.BankId).oRTGSCharge);
                
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool TransferAmountIMPS(string fromId, string toId, float amount)
        {
            var fromCustomer = dbContext.CustomerAccounts.Find(fromId);
            var toCustomer = dbContext.CustomerAccounts.Find(toId);

            if (fromCustomer.Balance - amount >= 0)
            {
                float fromBalance = fromCustomer.Balance;
                fromCustomer.Balance = fromBalance - amount;
                float toBalance = toCustomer.Balance;
                _ = toCustomer.BankId == fromCustomer.BankId ? toCustomer.Balance = toBalance + amount - (amount * dbContext.Banks.Find(toCustomer.BankId).sIMPSCharge) : toCustomer.Balance = toBalance + amount - (amount * dbContext.Banks.Find(fromCustomer.BankId).oIMPSCharge);
                
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

     

        public bool UpdatesRTGS(float sRTGSCharge, string bankId)
        {
            var bank = dbContext.Banks.Find(bankId);
            bank.sRTGSCharge = sRTGSCharge;
            dbContext.SaveChanges();
            return true;
        }

        public bool UpdatesIMPS(float sIMPSCharge, string bankId)
        {
            var bank = dbContext.Banks.Find(bankId);
            bank.sIMPSCharge = sIMPSCharge;
            dbContext.SaveChanges();
            return true;
        }

        public bool UpdateoRTGS(float oRTGSCharge, string bankId)
        {
            var bank = dbContext.Banks.Find(bankId);
            bank.oRTGSCharge = oRTGSCharge;
            dbContext.SaveChanges();
            return true;
        }

        public bool UpdateoIMPS(float oIMPSCharge, string bankId)
        {
            var bank = dbContext.Banks.Find(bankId);
            bank.oIMPSCharge = oIMPSCharge;
            dbContext.SaveChanges();
            return true;
        }

        

        

    }

}
