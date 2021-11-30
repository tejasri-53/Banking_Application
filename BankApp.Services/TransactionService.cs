using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using BankApp.Models;
using BankApp.Models.Exceptions;
using MySql;
using BankApp.Services;
using MySql.Data.MySqlClient;
using ConsoleTables;
using System.Transactions;

namespace BankApp.Services
{
    public class TransactionService
    {
        
        public static string GetDateTimeNow(bool forId)
        {
            return forId ? DateTime.Now.ToString("ddMMyyyyHHmmss") : DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        public static string DepositAmount(string accountId, float amount)
        {
            try
            {
                float currentBalance = AccountService.GetBalance(accountId);
                float newBalance = currentBalance +amount;

                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdateBalance, newBalance, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        
                        return AccountService.GetName(accountId);
                    }
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public static bool WithdrawAmount(string accountId, float amount)
        {
            try
            {
                float newBalance = AccountService.GetBalance(accountId) - amount;
                if (newBalance < 0)
                {
                    return false;
                }
                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdateBalance, newBalance, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        

        public static bool TransferAmountRTGS(string fromId, string toId, float amount)
        {
            float UpdatedAmount;
            string fromBankId = BankService.GetBankId(fromId);

            if (AccountService.GetBalance(fromId) - amount < 0)
            {
                return false;
            }
            if (fromBankId == BankService.GetBankId(toId))
            {
                float temp = amount * (BankService.GetBanksRTGSCharges(fromBankId) / 100);
                UpdatedAmount = amount - temp;
            }
            else
            {
                float temp = amount * (BankService.GetBankoRTGSCharges(fromBankId) / 100);
                UpdatedAmount = amount - temp;
            }

            

            AccountService.UpdateBalance(fromId, AccountService.GetBalance(fromId) - amount);
            AccountService.UpdateBalance(toId, AccountService.GetBalance(toId) + UpdatedAmount);

            return true;
        }

        public static bool TransferAmountIMPS(string fromId, string toId, float amount)
        {
            float UpdatedAmount;
            string fromBankId = BankService.GetBankId(fromId);

            if (AccountService.GetBalance(fromId) - amount < 0)
            {
                return false;
            }
            if (fromBankId == BankService.GetBankId(toId))
            {
                float temp = amount * (BankService.GetBanksIMPSCharges(fromBankId) / 100);
                UpdatedAmount = amount - temp;
            }
            else
            {
                float temp = amount * (BankService.GetBankoIMPSCharges(fromBankId) / 100);
                UpdatedAmount = amount - temp;
            }

            

            AccountService.UpdateBalance(fromId, AccountService.GetBalance(fromId) - amount);
            AccountService.UpdateBalance(toId, AccountService.GetBalance(toId) + UpdatedAmount);

            

            return true;
        }
        

    }
}