using System;
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
using BankApp.Services;
using MySql.Data.MySqlClient;

namespace BankApp.Services
{
    public class AccountService
    {
         public static  BankService bankService;
        
        public static string CreateCustomerAccount(string name, string password, string bankId)
        {
            string accountId = $"{name.Substring(0, 3)}{DateTime.Now.ToString("ddMMyyyyHHmmss")}";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.InsertIntoCustomersTable, accountId, bankId, 0, name, password), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }

            return accountId;
        }
        public static string CreateStaffAccount(string accountId, string name, string password, string bankId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.InsertIntoStaffsTable, accountId, bankId, name, password), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }

            return accountId;
        }


        public static float GetBalance(string accountId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetBalance, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        string temp = "";
                        while (reader.Read())
                        {
                            temp += reader.GetString(0);
                        }

                        return Convert.ToInt32(temp);

                    }
                }
            }
            catch
            {
                return -1;
            }
        }

        public static bool UpdateBalance(string accountId, float newBalance)
        {
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
        public static string GetName(string accountId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetName, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        string temp = "";
                        while (reader.Read())
                        {
                            temp += reader.GetString(0);
                        }

                        return temp;
                    }
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        
        public static bool DeleteCustomerAccount(string accountId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.DeleteCustomerAccount, accountId), conn))
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
        public static string UpdateCustomerPassword(string accountId, string newPassword)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdatePassword, newPassword, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        return newPassword;

                    }
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public static string UpdateCustomerName(string accountId, string newName)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdateName, newName, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        return newName;

                    }
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public static bool AuthenticateCustomer(string accountId, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AuthenticateCustomer, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        string temp = "";
                        while (reader.Read())
                        {
                            temp += reader.GetString(0);
                        }

                        return temp == "1" ? true : false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool Authenticatestaff(string accountId, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(BankService.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AuthenticateStaff, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        string temp = "";
                        while (reader.Read())
                        {
                            temp += reader.GetString(0);
                        }

                        return temp == "1" ? true : false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        
        
    }
}