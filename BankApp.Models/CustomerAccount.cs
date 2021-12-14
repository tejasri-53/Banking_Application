using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class CustomerAccount
    {
        public List<Transaction> Transactions = new List<Transaction>();

        public string BankId { get; set; }

      
        public string AccountId { get; set; }

        public float Balance { get; set; }

        
        public string Name { get; set; }

        
        public string Password { get; set; }

        
        public int IsActive { get; set; }

        /*
        public CustomerAccount(string name)
        {
            this.AccountId = $"{name.Substring(0, 3)}{DateTime.Now.ToString("ddMMyyyyHHmmss")}";
            this.Name = name;
            this.Balance = 0;
        }*/

        
    }
}
