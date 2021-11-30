using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;
using ConsoleTables;
using  BankApp.Models;

using BankApp.Services;

namespace BankApp.Services
{
    public class BankService
    {
        public static string connStr;

        public static string GetDateTimeNow(bool forId)
        {
            return forId ? DateTime.Now.ToString("ddMMyyyyHHmmss") : DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public string init()
        {
            connStr = "server=localhost;user=root;database=bankapp;port=3306;password=Tejasri53!";
            
            try
            {




                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CheckTabelsExist, conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        string temp = "";
                        while (reader.Read())
                        {
                            temp += reader.GetString(0);
                        }
                        if (temp != "1" || temp == null)
                        {
                            string x=CreateTables();
                            string bankId=AddBank("AndhraBank", 0, 5, 2, 6);
                            AccountService.CreateStaffAccount("admin", "admin", "admin", bankId);
                            return x;
                        }
                        return "Tables already exist !!";
                    }
                }
            }
            catch (Exception e)
            {
                return $"SQL ERROR WHILE INITILIZING DB \n{e}";
            }
        }

        public string CreateTables()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CreateBanksTable, conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }
                }

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CreateCustomerAccountsTable, conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }

                }
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CreateStaffAccountsTable, conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }

                }
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CreateTransactionsTable, conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }

                }

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CreateCurrencyTable, conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }

                }

                return "Succesfully created all tables !!";
            }
            catch (Exception e)
            {
                return "SQL ERROR: " + e.ToString();
            }
        }







        public string AddBank(string name)
        {
            string bankId = $"{name.Substring(0, 3)}{GetDateTimeNow(true)}";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.InsertIntoBanksTable, bankId, name, 0, 5, 2, 6), conn))
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
            return bankId;
        }

        public static string AddBank(string name, float sRTGS, float sIMPS, float oRTGS, float oIMPS)
        {
            string bankId = $"{name.Substring(0, 3)}{GetDateTimeNow(true)}";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.InsertIntoBanksTable, bankId, name, sRTGS, sIMPS, oRTGS, oIMPS), conn))
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
            return bankId;
        }

        public static string GetName(string accountId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
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




        public static float UpdatesRTGS(float val, string bankId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdatesRTGS, val, bankId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        return val;


                    }
                }
            }
            catch
            {
                return -1;
            }
        }

        
        public static float UpdatesIMPS(float val, string bankId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdatesIMPS, val, bankId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        return val;

                    }
                }
            }
            catch
            {
                return -1;
            }
        }

        public static float UpdateoRTGS(float val, string bankId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdateoRTGS, val, bankId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        return val;

                    }
                }
            }
            catch
            {
                return -1;
            }
        }

        public static float UpdateoIMPS(float val, string bankId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdateoIMPS, val, bankId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        return val;

                    }
                }
            }
            catch
            {
                return -1;
            }
        }

        public static float GetBanksRTGSCharges(string bankId)
        {

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetBanksRTGSCharges, bankId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    string temp = "";
                    while (reader.Read())
                    {
                        temp += reader.GetString(0);
                    }

                    return float.Parse(temp);
                }
            }
        }

        public static float GetBanksIMPSCharges(string bankId)
        {

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetBanksIMPSCharges, bankId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    string temp = "";
                    while (reader.Read())
                    {
                        temp += reader.GetString(0);
                    }

                    return float.Parse(temp);
                }
            }
        }

        public static float GetBankoRTGSCharges(string bankId)
        {

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetBankoRTGSCharges, bankId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    string temp = "";
                    while (reader.Read())
                    {
                        temp += reader.GetString(0);
                    }

                    return float.Parse(temp);
                }
            }
        }

        public static float GetBankoIMPSCharges(string bankId)
        {

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetBankoIMPSCharges, bankId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    string temp = "";
                    while (reader.Read())
                    {
                        temp += reader.GetString(0);
                    }

                    return float.Parse(temp);
                }
            }
        }



        public static string GetBankId(string accountId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetBankId, accountId), conn))
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

        
        

        


    }
}
