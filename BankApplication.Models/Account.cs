using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class Account
    {
        private readonly List<Transaction> transactions = new List<Transaction>();

        public string BankId { get; set; }

        public string AccountId { get; set; }
        public int Balance { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }

        public List<Transaction> GetTransactions() {
            return transactions;
        }

        public Account(string name)
        {
            this.AccountId = Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyHHmmss");

            this.Name = name;
            this.Balance = 0;
        }


        public bool AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
            return true;
        }

    }
    public class StaffAccount
    {

        public string AccountID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public StaffAccount( string name, string password)
        {
            this.Name = name;
            this.AccountID = Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
            this.Password = password;
        }

    }

}
